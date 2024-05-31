using UniSync.Application.Models.Identity;


namespace UniSync.Application.Contracts.Interfaces
{
    public interface IRoleAssignmentService
    {
        public UserInfoDto GetUserInfoByRegistrationId(string registrationId);
    }
}
public class UserInfoDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }

    public string? Semester { get; set; }
    public string? Group { get; set; }
    public List<string>? Courses { get; set; }
}