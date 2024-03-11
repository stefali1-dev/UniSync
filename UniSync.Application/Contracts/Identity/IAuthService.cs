using UniSync.Application.Models.Identity;

namespace UniSync.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
        Task<(int, string)> Logout();
        Task<(int, string)> ResetPassword(ResetPasswordModel model);
        Task<(int, string)> LoginWithGoogle(string googleToken);

    }
}
