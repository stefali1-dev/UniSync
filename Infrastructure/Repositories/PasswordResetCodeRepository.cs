using UniSync.Application.Persistence;
using UniSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UniSync.Infrastructure.Repositories;
using UniSync.Infrastructure;

namespace Infrastructure.Repositories
{
    public class PasswordResetCodeRepository : BaseRepository<PasswordResetCode>, IPasswordResetCode
    {
        public PasswordResetCodeRepository(UniSyncContext context) : base(context)
        {

        }

        public async Task<bool> HasValidCodeByEmailAsync(string email,string code)
        {

            return await context.PasswordResetCodes
                .AnyAsync(c => c.Email == email && c.Code == code && c.ExpirationTime > DateTime.UtcNow);
        }
        public async Task InvalidateExistingCodesAsync(string email)
        {
            var codesToInvalidate = await context.PasswordResetCodes
                .Where(code => code.Email == email && code.ExpirationTime > DateTime.UtcNow)
                .ToListAsync();

            foreach (var code in codesToInvalidate)
            {
                code.Invalidate();
            }

            await context.SaveChangesAsync();
        }
    }
}
