using UniSync.Domain.Common;
using UniSync.Domain.Entities;

namespace UniSync.Domain.Entities.Administration
{
    public class Course
    {
        public Guid CourseId { get; private set; }
        public string CourseName { get; private set; }
        public string CourseNumber { get; private set; }
        public int Credits { get; private set; }
        public CourseType Type { get; private set; }
        public string Description { get; private set; }
        public int Semester { get; private set; }
        public virtual List<Professor> Professors { get; private set; }
        public virtual List<Student> Students { get; private set; }

    }
}
