using FluentAssertions;
using UniSync.Application.Features.Categories.Queries.GetAll;
using UniSync.Application.Persistence;
using Moq;

namespace GlobalBuy.Ticket.Application.Tests.Queries
{
    public class GetAllCategoriesQueryHandlerTests : IDisposable
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public GetAllCategoriesQueryHandlerTests()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
        }

        [Fact]
        public async Task GetAllCategoriesQueryHandler_Success()
        {
            // Arrange
            var handler = new GetAllCategoriesQueryHandler(_mockCategoryRepository.Object);
            
            // Act
            var result = await handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

            // Assert
           result.Should().NotBeNull();
            result.Categories.Should().NotBeNullOrEmpty();
            result.Categories.Count.Should().Be(3);
        }

        public void Dispose()
        {
            _mockCategoryRepository.Reset();
        }
    }
}
