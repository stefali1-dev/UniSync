using UniSync.Domain.Common;

namespace UniSync.Domain.Entities.Actors
{
    public class Student : User
    {
        private Student(Guid userId, string firstName, string lastName, DateTime enrollmentDate, int semester, string group)
            : base(userId, firstName, lastName)
        {
            EnrollmentDate = enrollmentDate;
            Semester = semester;
            Group = group;
        }

        public DateTime EnrollmentDate { get; private set; }
        public int Semester { get; private set; }
        public string Group { get; private set; }

        public static Result<Student> Create(Guid userId, string firstName, string lastName, DateTime enrollmentDate, int Semester, string group)
        {
            if (userId == Guid.Empty)
            {
                return Result<Student>.Failure("UserId cannot be empty.");
            }

            return Result<Student>.Success(new Student(userId, firstName, lastName, enrollmentDate, Semester, group));
        }
    }
}
