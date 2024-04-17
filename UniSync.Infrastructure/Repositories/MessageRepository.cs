using UniSync.Application.Persistence;
using UniSync.Infrastructure.Repositories;
using UniSync.Infrastructure;
using UniSync.Domain.Entities;
using UniSync.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace UniSync.Infrastructure.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(UniSyncContext context) : base(context)
        {
        }

        public async Task<Result<IReadOnlyList<Message>>> GetMessagesByChannelAsync(string channel)
        {
            var messages = await context.Messages
                .Where(s => s.ChannelName == channel)
                .AsNoTracking()
                .ToListAsync();

            if (messages.Count == 0)
            {
                return Result<IReadOnlyList<Message>>.Failure($"Messages from channel {channel} not found");
            }

            return Result<IReadOnlyList<Message>>.Success(messages);
        }
    }
}
