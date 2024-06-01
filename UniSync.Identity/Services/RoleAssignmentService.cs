using UniSync.Application.Contracts.Interfaces;
using UniSync.Domain.Entities;
using UniSync.Identity.Models;

namespace UniSync.Identity.Services
{
    public class RoleAssignmentService : IRoleAssignmentService
    {



        public UserInfoDto GetUserInfoByRegistrationId(string registrationId)
        {
            if (registrationId.StartsWith('3'))
            {
                // student
                string studentsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "students.csv");
                if (!File.Exists(studentsFilePath))
                {
                    Console.WriteLine("Didn't find csv file");
                    return null;
                }

                string[] lines = File.ReadAllLines(studentsFilePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts[0] == registrationId)
                    {

                        // student
                        UserInfoDto student = new UserInfoDto
                        {
                            FirstName = parts[1],
                            LastName = parts[2],
                            Semester = parts[3],
                            Group = parts[4],
                            Role = UserRoles.Student,
                            CoursesIds = RetrieveStudentCourses(registrationId)
                        };

                        return student;


                    }
                }

            }


            if (registrationId.StartsWith('2'))
            {
                // professor
                string professorsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "professors.csv");
                if (!File.Exists(professorsFilePath))
                {
                    Console.WriteLine("Didn't find csv file");
                    return null;
                }

                string[] lines = File.ReadAllLines(professorsFilePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts[0] == registrationId)
                    {

                        // professor
                        UserInfoDto professor = new UserInfoDto
                        {
                            FirstName = parts[1],
                            LastName = parts[2],
                            Role = UserRoles.Professor,
                            CoursesIds = RetrieveProfessorCourses(registrationId)
                        };

                        return professor;


                    }
                }
            }

            if (registrationId.StartsWith('1'))
            {
                // admins
                string adminsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "admins.csv");
                if (!File.Exists(adminsFilePath))
                {
                    Console.WriteLine("Didn't find csv file");
                    return null;
                }

                string[] lines = File.ReadAllLines(adminsFilePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts[0] == registrationId)
                    {

                        // admin
                        UserInfoDto admin = new UserInfoDto
                        {
                            FirstName = parts[1],
                            LastName = parts[2],
                            Role = UserRoles.Admin
                        };

                        return admin;


                    }
                }
            }

            return null;
        }

        public List<string> RetrieveStudentCourses(string registrationId)
        {
            var courseIds = new List<string>();
            string studentsCoursesFilePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "students_courses.csv");

            // Read all lines from the CSV file
            var lines = File.ReadAllLines(studentsCoursesFilePath);

            // Skip the header line and process each line
            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(',');
                if (values[0].Trim() == registrationId)
                {
                    courseIds.Add(values[1].Trim());
                }
            }

            return courseIds;
        }

        public List<string> RetrieveProfessorCourses(string registrationId)
        {
            var courseIds = new List<string>();
            string professorsCoursesFilePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "professors_courses.csv");

            // Read all lines from the CSV file
            var lines = File.ReadAllLines(professorsCoursesFilePath);

            // Skip the header line and process each line
            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(',');
                if (values[0].Trim() == registrationId)
                {
                    courseIds.Add(values[1].Trim());
                }
            }

            return courseIds;
        }
    }
    
}
