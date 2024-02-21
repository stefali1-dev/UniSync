using API.Entities;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace APITest.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class UsersControllerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UsersController _controller;
    private readonly ITestOutputHelper _output;

    public UsersControllerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockMapper = new Mock<IMapper>();
        _controller = new UsersController(_mockUserRepository.Object, _mockMapper.Object);
        _output = new TestOutputHelper();
    }

    [Fact]
    public async Task GetUsers_ReturnsOkObjectResult_WithAListOfMemberDtos()
    {
        // Arrange
        var fakeUsers = new List<User> { new User(), new User() };
        var fakeMemberDtos = new List<MemberDto> { new MemberDto(), new MemberDto() };
        
        _mockUserRepository.Setup(repo => repo.GetUsersAsync()).ReturnsAsync(fakeUsers);
        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<MemberDto>>(It.IsAny<IEnumerable<User>>())).Returns(fakeMemberDtos);

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<MemberDto>>(actionResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetUser_ReturnsOkObjectResult_WithMemberDto()
    {
        // Arrange
        var fakeUser = new User { UserId = 1 };
        var fakeMemberDto = new MemberDto
        {
            UserId = 1,
            Email = "first@gmail.com"
        };

        _mockUserRepository.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(fakeUser);
        _mockMapper.Setup(mapper => mapper.Map<MemberDto>(It.IsAny<User>())).Returns(fakeMemberDto);

        // Act
        var result = await _controller.GetUser(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<MemberDto>>(result);
        Assert.IsNotType<NotFoundResult>(actionResult.Result);
        Assert.Equal(fakeMemberDto, actionResult.Value); 
    }
}
