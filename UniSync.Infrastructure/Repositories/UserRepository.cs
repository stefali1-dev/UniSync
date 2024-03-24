using UniSync.Application.Persistence;
using UniSync.Infrastructure.Repositories;
using UniSync.Infrastructure;
using UniSync.Domain.Entities;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(UniSyncContext context) : base(context)
    {

    }


}