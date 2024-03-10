using UniSync.Application.Models;

namespace UniSync.Application.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Mail email);
    }
}
