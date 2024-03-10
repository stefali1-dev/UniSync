using UniSync.Application.Contracts.Interfaces;
using Microsoft.Identity.Web;
using System.Security.Claims;
using UniSync.Application.Models.Identity;

namespace WebAPI.Services
{
    public class RoleAssignmentService : IRoleAssignmentService
    {

        public string GetUserRoleByRegistrationId(string registrationId)
        {
            // TODO: Implement real logic
            if(registrationId.EndsWith("1"))
            {
                return UserRole.Admin;
            }
            else if(registrationId.EndsWith("2"))
            {
                return UserRole.User;
            }
            else if(registrationId.EndsWith("3"))
            {
                return UserRole.Student;
            }
            else if(registrationId.EndsWith("4"))
            {
                return UserRole.Professor;
            }
            else if(registrationId.EndsWith("5"))
            {
                return UserRole.Staff;
            }
            else
            {
                return "Invalid Registration ID";
            }
        }
    }
    
}
