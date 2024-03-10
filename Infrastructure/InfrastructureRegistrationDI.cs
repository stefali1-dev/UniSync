using UniSync.Application.Contracts;
using UniSync.Application.Persistence;
using UniSync.Infrastructure.Repositories;
using UniSync.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UniSync.Infrastructure
{
    public static class InfrastructureRegistrationDI
    {
        public static IServiceCollection AddInfrastrutureToDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<UniSyncContext>(
                options =>
                options.UseNpgsql(
                    configuration.GetConnectionString
                    ("GlobalTicketsConnection"),
                    builder =>
                    builder.MigrationsAssembly(
                        typeof(UniSyncContext)
                        .Assembly.FullName)));
            
            services.AddScoped
                (typeof(IAsyncRepository<>),
                typeof(BaseRepository<>));
            services.AddScoped
                <ICategoryRepository, CategoryRepository>();
            services.AddScoped
                <IEventRepository, EventRepository>();
            services.AddScoped
                <IEmailService, EmailService>();
            return services;
        }
    }
}
