using UniSync.Application.Persistence;
using UniSync.Domain.Common;
using UniSync.Domain.Entities;
using Moq;

namespace GlobalBuy.Ticket.Application.Tests
{
    public class RepositoryMocks
    {
        public static Mock<ICategoryRepository> GetCategoryRepository()
        {
            var categories = new List<Category>
           {
               Category.Create("Concert").Value,
               Category.Create("Musical").Value,
               Category.Create("Conferences").Value
           };
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(Result<IReadOnlyList<Category>>.Success(categories));

            return mockCategoryRepository;
        }
    }
}