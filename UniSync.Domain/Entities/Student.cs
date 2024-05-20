using UniSync.Domain.Entities.Administration;

namespace UniSync.Domain.Entities
{
    public class Student
    {

        public Guid StudentId { get; set; }
        public Guid ChatUserId { get; set; }
        public int Semester { get; set; }
        public string Group { get; set; }
        //public ICollection<Course> Courses { get; set; }
    }
}
