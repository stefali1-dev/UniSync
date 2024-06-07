using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Persistence;
using UniSync.Domain.Entities;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Application.Features.Courses
{
    public class CoursesService : ICoursesService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IProfessorRepository professorRepository;
        private readonly IStudentRepository studentRepository;

        public CoursesService(ICourseRepository courseRepository, IProfessorRepository professorRepository, IStudentRepository studentRepository)
        {
            this.courseRepository = courseRepository;
            this.professorRepository = professorRepository;
            this.studentRepository = studentRepository;
        }

        public async Task LoadCoursesFromCsv(string csvPath)
        {

            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(csvPath);

            // Process each line except for the header
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');

                // TODO: Add course students and professors
                Course course = new Course(
                    Guid.NewGuid(),
                    values[1], // courseName
                    values[0], // courseNumber
                    int.Parse(values[2]), // credits
                    int.Parse(values[3]) // semester
                );

                var result = await courseRepository.AddAsync(course);

            }

        }

        public async Task<List<CourseDto>> GetCoursesByProfessorId(string professorId)
        {
            try
            {
                var result = await professorRepository.FindByIdAsync(new Guid(professorId));

                var courseDtoList = new List<CourseDto>();

                foreach (Course c in result.Value.Courses)
                {
                    CourseDto courseDto = new CourseDto
                    {
                        CourseId = c.CourseId.ToString(),
                        CourseName = c.CourseName,
                        CourseNumber = c.CourseNumber,
                        Credits = c.Credits,
                        Description = c.Description,
                        Semester = c.Semester,
                        ProfessorsIds = c.Professors.Select(p => p.ProfessorId.ToString()).ToList(),
                        StudentsIds = c.Students.Select(p => p.StudentId.ToString()).ToList()
                    };

                    courseDtoList.Add(courseDto);
                }
                return courseDtoList;

            }
            catch (Exception ex)
            {
                return new List<CourseDto>();
            }

        }
        public async Task<List<CourseDto>> GetCoursesByStudentId(string studentId)
        {
            try
            {
                var result = await studentRepository.FindByIdAsync(new Guid(studentId));

                var courseDtoList = new List<CourseDto>();

                foreach (Course c in result.Value.Courses)
                {
                    CourseDto courseDto = new CourseDto
                    {
                        CourseId = c.CourseId.ToString(),
                        CourseName = c.CourseName,
                        CourseNumber = c.CourseNumber,
                        Credits = c.Credits,
                        Description = c.Description,
                        Semester = c.Semester,
                        ProfessorsIds = c.Professors.Select(p => p.ProfessorId.ToString()).ToList(),
                        StudentsIds = c.Students.Select(p => p.StudentId.ToString()).ToList()
                    };

                    courseDtoList.Add(courseDto);
                }
                return courseDtoList;

            }
            catch (Exception ex)
            {
                return new List<CourseDto>();
            }

        }
    }
}
