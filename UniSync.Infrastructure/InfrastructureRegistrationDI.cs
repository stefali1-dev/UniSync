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
                    configuration.GetConnectionString("UniSyncConnection"),
                    builder =>
                    builder.MigrationsAssembly(typeof(UniSyncContext).Assembly.FullName))
                .EnableSensitiveDataLogging() // Add this line to enable sensitive data logging
            );

            services.AddScoped
                (typeof(IAsyncRepository<>),
                typeof(BaseRepository<>));
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IChannelRepository, ChannelRepository>();
            services.AddScoped<IPasswordResetCode, PasswordResetCodeRepository>();
            services.AddScoped<IUserPhotoRepository, UserPhotoRepository>();
            services.AddScoped<IChatUserRepository, ChatUserRepository>();
            services.AddScoped<IProfessorRepository,ProfessorRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IEvaluationRepository, EvaluationRepository>();

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
