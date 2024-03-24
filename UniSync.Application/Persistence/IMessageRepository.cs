using UniSync.Domain.Entities;

namespace UniSync.Application.Persistence
{
    public interface IMessageRepository : IAsyncRepository<Message>
    {
    }
}
