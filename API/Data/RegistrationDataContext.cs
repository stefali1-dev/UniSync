using API.Entities;

namespace API.Data;

using Microsoft.EntityFrameworkCore;

public class RegistrationDataContext : DbContext
{
    public DbSet<UserRegistrationRole> UserRegistrationRoles { get; set; }

    public RegistrationDataContext(DbContextOptions<RegistrationDataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure composite key for UserRegistrationRole
        modelBuilder.Entity<UserRegistrationRole>()
            .HasKey(ur => new { ur.RegistrationId, ur.RoleId });
    }
}
