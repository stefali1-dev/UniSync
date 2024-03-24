using UniSync.Application.Persistence;
using UniSync.Infrastructure.Repositories;
using UniSync.Infrastructure;
using UniSync.Domain.Entities;

namespace UniSync.Infrastructure.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(UniSyncContext context) : base(context)
        {
        }
    }
}
