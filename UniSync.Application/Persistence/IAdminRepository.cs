using UniSync.Domain.Common;
using UniSync.Domain.Entities;

namespace UniSync.Application.Persistence
{
    public interface IAdminRepository : IAsyncRepository<Admin>
    {
    }
}
