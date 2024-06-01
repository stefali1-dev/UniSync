using UniSync.Domain.Common;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Domain.Entities
{
    public class Professor
    {

        public Guid ProfessorId { get; set; }
        public Guid ChatUserId { get; set; }

        public ProfessorType Type { get; set; }
        public virtual List<Course> Courses { get; set; } = new List<Course>();

        public void AttatchCourse(Course course)
        {
            if (Courses == null)
            {
                Courses = new List<Course>();

            }

            Courses.Add(course);
        }

    }
}