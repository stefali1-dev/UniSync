using UniSync.Domain.Entities.Actors;

namespace UniSync.Application.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}
