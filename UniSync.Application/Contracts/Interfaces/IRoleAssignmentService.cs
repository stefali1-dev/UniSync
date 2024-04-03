using UniSync.Application.Models.Identity;


namespace UniSync.Application.Contracts.Interfaces
{
    public interface IRoleAssignmentService
    {
        public StudentDto GetUserInfoByRegistrationId(string registrationId);
    }
}
public class StudentDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Semester { get; set; }
    public string Group { get; set; }
}