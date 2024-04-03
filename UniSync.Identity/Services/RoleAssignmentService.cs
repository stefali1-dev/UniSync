using UniSync.Application.Contracts.Interfaces;
using UniSync.Identity.Models;

namespace UniSync.Identity.Services
{
    public class RoleAssignmentService : IRoleAssignmentService
    {



        public StudentDto GetUserInfoByRegistrationId(string registrationId)
        {
            string studentsFilePath = "../TestData/students.txt"; // file path

            if (!File.Exists(studentsFilePath))
            {
                return null;
            }

            string[] lines = File.ReadAllLines(studentsFilePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts[0] == registrationId)
                {
                    StudentDto student = new StudentDto
                    {
                        FirstName = parts[1],
                        LastName = parts[2],
                        Semester = parts[3],
                        Group = parts[4]
                    };

                    return student;
                }
            }

            return null;
        }
    }
    
}
