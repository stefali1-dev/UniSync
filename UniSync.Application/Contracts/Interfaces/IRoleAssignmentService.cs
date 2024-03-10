using UniSync.Application.Models.Identity;


namespace UniSync.Application.Contracts.Interfaces
{
    public interface IRoleAssignmentService
    {
        public string GetUserRoleByRegistrationId(string registrationId);
    }
}
