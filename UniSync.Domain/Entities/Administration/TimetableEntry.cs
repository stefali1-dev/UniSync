namespace UniSync.Domain.Entities.Administration
{
    public class TimetableEntry
    {
        public Guid TimetableEntryId { get; set; }
        public string TimeInterval { get; set; }
        public Guid CourseId {  get; set; }
        public string CourseName { get; set; }
        public string CourseType { get; set; }

        public Guid ProfessorId { get; set; }
        public string ProfessorName { get; set; }
        public string Classroom { get; set;}
        
        public int DayOfWeek { get; set; }
        public string StudentGroup { get; set; }
    }
}
