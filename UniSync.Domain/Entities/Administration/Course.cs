using UniSync.Domain.Common;
using UniSync.Domain.Entities;

namespace UniSync.Domain.Entities.Administration
{
    public class Course
    {
        public Course(Guid courseId, string courseName, string courseNumber, int credits, int semester)
        {
            CourseId = courseId;
            CourseName = courseName;
            CourseNumber = courseNumber;
            Credits = credits;
            Semester = semester;
            Description = "";
            Professors = new List<Professor>();
            Students = new List<Student>();

        }

        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseNumber { get; set; }
        public int Credits { get; set; }
        //public CourseType? Type { get; set; }
        public string Description { get; set; }
        public int Semester { get; set; }
        public virtual List<Professor> Professors { get; set; }
        public virtual List<Student> Students { get; set; }

        public void AttachStudent(Student student)
        {
            if (student != null)
            {
                Students.Add(student);
            }
        }

        public void AttachProfessor(Professor professor)
        {
            if (professor != null)
            {
                Professors.Add(professor);
            }
        }
    }
}
