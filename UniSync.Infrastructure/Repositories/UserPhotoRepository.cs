using UniSync.Application.Persistence;
using UniSync.Domain.Common;
using Microsoft.EntityFrameworkCore;
using UniSync.Infrastructure.Repositories;
using UniSync.Infrastructure;
using Ergo.Domain.Entities;

namespace Infrastructure.Repositories
{
    public class UserPhotoRepository : BaseRepository<UserPhoto>, IUserPhotoRepository
    {
        public UserPhotoRepository(UniSyncContext context) : base(context)
        {
        }

        public async Task<Result<UserPhoto>> GetUserPhotoByUserIdAsync(string userId)
        {
            //var userPhoto = await context.UserPhotos
            //                    .Where(up => up.UserId == userId)
            //                    .FirstOrDefaultAsync();

            //if (userPhoto == null)
            //{
            //    return Result<UserPhoto>.Failure("User photo not found");
            //}

            //return Result<UserPhoto>.Success(userPhoto);
            return Result<UserPhoto>.Failure("User photo not found");


        }
    }
}
