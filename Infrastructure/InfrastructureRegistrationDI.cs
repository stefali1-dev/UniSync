using UniSync.Application.Contracts;
using UniSync.Application.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniSync.Infrastructure.Repositories;
using UniSync.Infrastructure;

namespace Infrastructure
{
    public static class InfrastructureRegistrationDI
    {
        public static IServiceCollection AddInfrastructureToDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<UniSyncContext>(
                options =>
                options.UseNpgsql(
                    configuration.GetConnectionString
                    ("UniSyncConnection"),
                    builder =>
                    builder.MigrationsAssembly(
                        typeof(UniSyncContext)
                        .Assembly.FullName)));
            services.AddScoped
                (typeof(IAsyncRepository<>),
                typeof(BaseRepository<>));
            services.AddScoped<
                IUserRepository, UserRepository>();
            services.AddScoped<IPasswordResetCode, PasswordResetCodeRepository>();
            services.AddScoped<IUserPhotoRepository, UserPhotoRepository>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
