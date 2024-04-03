using UniSync.Domain.Entities.Administration;

namespace UniSync.Domain.Entities
{
    public class Student : User
    {
        public Student(Guid userId, int semester, string group) : base(userId)
        {
            Semester = semester;
            Group = group;
            Courses = new List<Course>();
        }

        public int Semester { get; set; }
        public string Group { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
