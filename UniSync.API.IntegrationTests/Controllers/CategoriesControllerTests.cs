using FluentAssertions;
using GlobalBuy.Ticket.API.IntegrationTests.Base;
using UniSync.Application.Features;
using UniSync.Application.Features.Categories.Commands.CreateCategory;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace GlobalBuy.Ticket.API.IntegrationTests.Controllers
{
    public class CategoriesControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/categories";

        [Fact]
        public async Task When_GetAllCategoriesQueryHandlerIsCalled_Then_Success()
        {
            // Arrange && Act
            var response = await Client.GetAsync(RequestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<CategoryDto>>(responseString);
            // Assert
            result?.Count().Should().Be(4);
        }

        [Fact]
        public async Task When_PostCategoryCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            // Arrange
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var category = new CreateCategoryCommand
            {
                CategoryName = "Test Category",
            };
            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, category);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CategoryDto>(responseString);
            result?.Should().NotBeNull();
            result?.CategoryName.Should().Be(category.CategoryName);
        }

        private static string CreateToken()
        {

            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
            new JwtSecurityToken(
                JwtTokenProvider.Issuer,
                JwtTokenProvider.Issuer,
                new List<Claim> { new(ClaimTypes.Role, "User"), new("department", "Security") },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: JwtTokenProvider.SigningCredentials
            ));
        }
    }
}
