using Microsoft.EntityFrameworkCore;
using UniSync.Application.Persistence;
using UniSync.Domain.Common;
using UniSync.Domain.Entities;

namespace UniSync.Infrastructure.Repositories
{
    public class ChannelRepository : BaseRepository<Channel>, IChannelRepository
    {
        public ChannelRepository(UniSyncContext context) : base(context)
        {
        }

        public async Task<Result<IReadOnlyList<Channel>>> GetChannelsByUserIdAsync(Guid userId)
        {
            var channels = await context.Channels
                .Where(c => c.Users.Any(u => u.UserId == userId))
                .AsNoTracking()
                .ToListAsync();

            if (channels.Count == 0)
            {
                return Result<IReadOnlyList<Channel>>.Failure($"Channels from user {userId} not found");
            }

            return Result<IReadOnlyList<Channel>>.Success(channels);
        }
    }
}
