using UniSync.Application.Contracts.Interfaces;
using UniSync.Identity.Models;

namespace UniSync.Identity.Services
{
    public class RoleAssignmentService : IRoleAssignmentService
    {



        public UserInfoDto GetUserInfoByRegistrationId(string registrationId)
        {
            string usersInfoFilePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "users_info.txt");
            Console.WriteLine(usersInfoFilePath);

            if (!File.Exists(usersInfoFilePath))
            {
                Console.WriteLine("Didn't find text file");
                return null;
            }

            Console.WriteLine("Read from text file");

            string[] lines = File.ReadAllLines(usersInfoFilePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts[0] == registrationId)
                {
                    if (registrationId.StartsWith('2'))
                    {
                        // professor
                        UserInfoDto professor = new UserInfoDto
                        {
                            FirstName = parts[1],
                            LastName = parts[2],
                            Courses = new List<string>(parts[3..])
                    };

                        return professor;
                    }

                    if (registrationId.StartsWith('3'))
                    {
                        // student
                        UserInfoDto student = new UserInfoDto
                        {
                            FirstName = parts[1],
                            LastName = parts[2],
                            Semester = parts[3],
                            Group = parts[4]
                        };

                        return student;
                    }


                }
            }

            return null;
        }
    }
    
}
