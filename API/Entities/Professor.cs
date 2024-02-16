namespace API.Entities;

public class Professor : User
{
    public string HireDate { get; set; }
    public string Department { get; set; }
    // Navigation properties
    public virtual ICollection<Course> Courses { get; set; }
    public virtual ICollection<Evaluation> Evaluations { get; set; }
}