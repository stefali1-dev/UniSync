using UniSync.Application.Models.Identity;

namespace UniSync.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);

        Task<(int, string)> Logout();

    }
}
