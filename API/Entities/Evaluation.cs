namespace API.Entities;

public class Evaluation
{
    public int EvaluationID { get; set; }
    public int StudentId { get; set; }
    public virtual Student Student { get; set; }
    public int ProfessorId { get; set; }
    public virtual Professor Professor { get; set; }
    public string Grade { get; set; }
    public string Attendance { get; set; }
    public double ExamScore { get; set; }
    public DateTime Timestamp { get; set; }
}