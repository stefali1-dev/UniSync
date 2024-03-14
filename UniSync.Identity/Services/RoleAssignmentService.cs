using UniSync.Application.Contracts.Interfaces;
using UniSync.Identity.Models;

namespace UniSync.Identity.Services
{
    public class RoleAssignmentService : IRoleAssignmentService
    {

        public string GetUserRoleByRegistrationId(string registrationId)
        {
            // TODO: Implement real logic
            if(registrationId.EndsWith("1"))
            {
                return UserRoles.Admin;
            }
            else if(registrationId.EndsWith("2"))
            {
                return UserRoles.User;
            }
            else if(registrationId.EndsWith("3"))
            {
                return UserRoles.Student;
            }
            else if(registrationId.EndsWith("4"))
            {
                return UserRoles.Professor;
            }
            else if(registrationId.EndsWith("5"))
            {
                return UserRoles.Staff;
            }
            else
            {
                return "Invalid Registration ID";
            }
        }
    }
    
}
