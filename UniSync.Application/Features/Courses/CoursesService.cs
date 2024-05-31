using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Persistence;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Application.Features.Courses
{
    public class CoursesService : ICoursesService
    {
        private readonly ICourseRepository courseRepository;

        public CoursesService(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task LoadCoursesFromCsv(string csvPath)
        {

            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(csvPath);

            // Process each line except for the header
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');

                // Create a new Course object and fill it with data
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
    }
}
