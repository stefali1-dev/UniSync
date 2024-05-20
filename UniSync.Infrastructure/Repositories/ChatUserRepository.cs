using UniSync.Application.Persistence;
using UniSync.Domain.Entities;

namespace UniSync.Infrastructure.Repositories
{
    public class ChatUserRepository : BaseRepository<ChatUser>, IChatUserRepository
    {
        public ChatUserRepository(UniSyncContext context) : base(context)
        {
        }
    }
}
