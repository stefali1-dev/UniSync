using UniSync.Domain.Entities.Administration;

namespace UniSync.Domain.Entities
{
    public class Student
    {
        public Student(Guid studentId, int semester, string group)
        {
            StudentId = studentId;
            Semester = semester;
            Group = group;
            //Courses = new List<Course>();
        }

        public Guid StudentId { get; set; }
        public int Semester { get; set; }
        public string Group { get; set; }
        //public ICollection<Course> Courses { get; set; }
    }
}
