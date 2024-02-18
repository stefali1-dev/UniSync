using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper =  mapper;
    } 
    
    [HttpGet] // GET: api/users
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await _userRepository.GetUsersAsync();
        
        var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
        
        return Ok(usersToReturn);
    }
    
    [HttpGet("{id}")] // GET: api/users/5
    public async Task<ActionResult<MemberDto>> GetUser(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        return _mapper.Map<MemberDto>(user);
    }
}