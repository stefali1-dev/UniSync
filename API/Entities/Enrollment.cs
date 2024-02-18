namespace API.Entities;

public class Enrollment
{
    public int EnrollmentID { get; set; }
    public int StudentId { get; set; }
    public virtual Student Student { get; set; }
    public int CourseId { get; set; }
    public virtual Course Course { get; set; }
    public DateTime EnrollmentDate { get; set; }
}