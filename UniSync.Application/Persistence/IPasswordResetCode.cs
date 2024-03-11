using UniSync.Domain.Entities;

namespace UniSync.Application.Persistence
{
    public interface IPasswordResetCode : IAsyncRepository<PasswordResetCode>
    {
        Task<bool> HasValidCodeByEmailAsync(string email,string code);
        Task InvalidateExistingCodesAsync(string email);
    }
}
