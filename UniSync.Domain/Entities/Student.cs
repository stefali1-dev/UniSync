using UniSync.Domain.Entities.Administration;

namespace UniSync.Domain.Entities
{
    public class Student
    {

        public Guid StudentId { get; set; }
        public Guid ChatUserId { get; set; }
        public int Semester { get; set; }
        public string Group { get; set; }
        public virtual List<Course> Courses { get; set; } = new List<Course>();
        public virtual List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

        public void AttatchCourse(Course course)
        {
            if (Courses == null) 
            {
                Courses = new List<Course>();

            }

            Courses.Add(course);
        }
        public void AttatchEvaluation(Evaluation evaluation)
        {
            if (Evaluations == null)
            {
                Evaluations = new List<Evaluation>();

            }

            Evaluations.Add(evaluation);
        }

    }
}
