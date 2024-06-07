namespace UniSync.Application.Features.Students
{
    public class StudentDto
    {
        public Guid StudentId { get; set; }
        public Guid ChatUserId { get; set; }
        public int Semester { get; set; }
        public string Group { get; set; }
        public List<string> CoursesIds { get; set; }
    }
}
