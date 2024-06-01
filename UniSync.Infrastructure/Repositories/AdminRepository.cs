using UniSync.Application.Persistence;
using UniSync.Infrastructure.Repositories;
using UniSync.Infrastructure;
using UniSync.Domain.Entities;
using UniSync.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    public AdminRepository(UniSyncContext context) : base(context)
    {

    }

}