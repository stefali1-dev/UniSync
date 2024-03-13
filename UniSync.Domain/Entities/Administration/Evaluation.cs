using UniSync.Domain.Common; // Assuming you have a similar structure for Result<T> here
using System;

namespace UniSync.Domain.Entities.Administration
{
    public class Evaluation
    {
        public Guid EvaluationId { get; private set; }
        public Guid StudentId { get; private set; }
        public Guid CourseId { get; private set; }
        public int Grade { get; private set; }
        public string? Comments { get; private set; }

    }
}
