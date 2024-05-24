using UniSync.Domain.Common;
using UniSync.Domain.Entities;

namespace UniSync.Application.Persistence
{
    public interface IMessageRepository : IAsyncRepository<Message>
    {
        Task<Result<IReadOnlyList<Message>>> GetMessagesByChannelAsync(Guid channelId);

    }
}
