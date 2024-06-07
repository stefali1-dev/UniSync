using UniSync.Domain.Common; // Assuming you have a similar structure for Result<T> here
using System;

namespace UniSync.Domain.Entities.Administration
{
    public class Evaluation
    {
        public Guid EvaluationId { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public Guid ProfessorId { get; set; }
        public int Grade { get; set; }
        public DateTime DateTime { get; set; }
        public string? Comments { get; set; }

    }
}
