using UniSync.Domain.Common;
using UniSync.Domain.Entities.Actors;

namespace UniSync.Domain.Entities.Administration
{
    public class Course
    {
        public Guid CourseId { get; private set; }
        public string CourseName { get; private set; }
        public int Credits { get; private set; }
        public string Type { get; private set; }
        public string Description { get; private set; }
        public int Semester { get; private set; }
        public List<Professor> Professors { get; private set; }
        public List<Student> Students { get; private set; }

        // Constructor
        private Course(Guid courseId, string courseName, int credits, string type, string description, int semester)
        {
            CourseId = courseId;
            CourseName = courseName;
            Credits = credits;
            Type = type;
            Description = description;
            Semester = semester;
            Professors = new List<Professor>();
            Students = new List<Student>();
        }

        // Create method
        public static Result<Course> Create(Guid courseId, string courseName, int credits, string type, string description, int semester)
        {
            if (courseId == Guid.Empty)
            {
                return Result<Course>.Failure("CourseId cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(courseName))
            {
                return Result<Course>.Failure("CourseName cannot be empty or whitespace");
            }

            if (credits <= 0)
            {
                return Result<Course>.Failure("Credits must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                return Result<Course>.Failure("Type cannot be empty or whitespace");
            }

            if (semester <= 0)
            {
                return Result<Course>.Failure("Semester must be greater than 0");
            }

            var course = new Course(courseId, courseName, credits, type, description, semester);
            return Result<Course>.Success(course);
        }
    }
}
