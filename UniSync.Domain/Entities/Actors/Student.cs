using UniSync.Domain.Entities.Administration;

namespace UniSync.Domain.Entities.Actors
{
    public class Student
    {
        // guid
        public Guid UserId { get; set; }
        public int Semester { get; set; }
        public string Group { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
