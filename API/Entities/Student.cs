namespace API.Entities;

public class Student : User
{
    public DateTime EnrollmentDate { get; set; }
    public string Major { get; set; }
    // Navigation properties
    public virtual ICollection<Enrollment> Enrollments { get; set; }
    public virtual ICollection<Evaluation> Evaluations { get; set; }
}