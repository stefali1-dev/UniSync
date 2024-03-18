using UniSync.Domain.Entities;

namespace UniSync.Application.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}
