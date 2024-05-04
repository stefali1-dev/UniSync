

using UniSync.Domain.Common;
using UniSync.Domain.Entities;

namespace UniSync.Application.Persistence
{
    public interface IChannelRepository : IAsyncRepository<Channel>
    {
        Task<Result<IReadOnlyList<Channel>>> GetChannelsByUserIdAsync(Guid userId);

    }
}
