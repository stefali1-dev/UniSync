using UniSync.Application.Persistence;
using UniSync.Domain.Entities;

namespace UniSync.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(UniSyncContext context) : base(context)
        {
        }
    }
}
