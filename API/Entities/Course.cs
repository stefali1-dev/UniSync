namespace API.Entities;

public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public int Credits { get; set; }
    public string Description { get; set; }
    public string Schedule { get; set; } // Consider using a more complex type or structure for real scheduling
    // Navigation properties
    public virtual ICollection<Enrollment> Enrollments { get; set; }
    public virtual ICollection<Resource> Resources { get; set; }
    public int ProfessorId { get; set; }
    public virtual Professor Professor { get; set; }
}