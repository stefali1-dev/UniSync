using UniSync.Domain.Entities.Administration;

namespace UniSync.Domain.Entities
{
    public class Student : User
    {
        public Student(Guid userId) : base(userId)
        {
        }

        public int Semester { get; set; }
        public string Group { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
