using Ergo.Domain.Entities;
using UniSync.Domain.Common;
using UniSync.Domain.Entities;

namespace UniSync.Application.Persistence
{
    public interface IUserPhotoRepository : IAsyncRepository<UserPhoto>
    {
        Task<Result<UserPhoto>> GetUserPhotoByUserIdAsync(string userId);
    }
}
