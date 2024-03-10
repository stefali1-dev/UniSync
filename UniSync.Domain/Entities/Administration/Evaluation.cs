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

        // Constructor
        private Evaluation(Guid evaluationId, Guid studentId, Guid courseId, int grade, string? comments)
        {
            EvaluationId = evaluationId;
            StudentId = studentId;
            CourseId = courseId;
            Grade = grade;
            Comments = comments;
        }

        // Create method
        public static Result<Evaluation> Create(Guid evaluationId, Guid studentId, Guid courseId, int grade, string? comments)
        {
            if (evaluationId == Guid.Empty)
            {
                return Result<Evaluation>.Failure("EvaluationId cannot be empty.");
            }

            if (studentId == Guid.Empty)
            {
                return Result<Evaluation>.Failure("StudentId cannot be empty");
            }

            if (courseId == Guid.Empty)
            {
                return Result<Evaluation>.Failure("CourseId cannot be empty");
            }

            if (grade < 4 || grade > 10) // Assuming grades are between 4 and 10
            {
                return Result<Evaluation>.Failure("Invalid Grade. It must be between 4 and 10.");
            }

            var evaluation = new Evaluation(evaluationId, studentId, courseId, grade, comments);
            return Result<Evaluation>.Success(evaluation);
        }
    }
}
