using UniSync.Application.Contracts.Interfaces;
using UniSync.Domain.Common;
using UniSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UniSync.Domain.Entities.Administration;
using UniSync.Domain.Entities;
using Ergo.Domain.Entities;

namespace UniSync.Infrastructure
{
    public class UniSyncContext : DbContext
    {
        private readonly ICurrentUserService currentUserService;

        public UniSyncContext(
            DbContextOptions<UniSyncContext> options, ICurrentUserService currentUserService) :
            base(options)
        {
            this.currentUserService = currentUserService;
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<PasswordResetCode> PasswordResetCodes { get; set; }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
