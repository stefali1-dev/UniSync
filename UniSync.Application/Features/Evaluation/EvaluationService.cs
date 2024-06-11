using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Persistence;

namespace UniSync.Application.Features.Evaluation
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IEvaluationRepository evaluationRepository;
        private readonly IStudentRepository studentRepository;

        public EvaluationService(IEvaluationRepository evaluationRepository, IStudentRepository studentRepository)
        {
            this.evaluationRepository = evaluationRepository;
            this.studentRepository = studentRepository; 
        }

        public async Task AddEvaluation(EvaluationDto evaluationDto)
        {
            var evaluation = new Domain.Entities.Administration.Evaluation
            {
                EvaluationId = Guid.NewGuid(),
                StudentId = Guid.Parse(evaluationDto.StudentId),
                CourseId = Guid.Parse(evaluationDto.CourseId),
                ProfessorId = Guid.Parse(evaluationDto.ProfessorId),
                Grade = evaluationDto.Grade,
                DateTime = evaluationDto.DateTime,
                Comment = evaluationDto.Comment
            };

            var result = await studentRepository.FindByChatUserId(Guid.Parse(evaluationDto.StudentId));
            var student = result.Value;

            student.AttatchEvaluation(evaluation);

            await evaluationRepository.AddAsync(evaluation);
        }

        public async Task<List<EvaluationDto>> GetByStudentIdAsync(string studentId)
        {
            try
            {
                var studentIdGuid = Guid.Parse(studentId);
                var result = await evaluationRepository.GetByStudentIdAsync(studentIdGuid);

                // Map the Evaluation entities to EvaluationDto objects
                var evaluationDtos = result.Value.Select(e => new EvaluationDto
                {
                    StudentId = e.StudentId.ToString(),
                    CourseId = e.CourseId.ToString(),
                    ProfessorId = e.ProfessorId.ToString(),
                    Grade = e.Grade,
                    DateTime = e.DateTime,
                    Comment = e.Comment
                }).ToList();

                return evaluationDtos;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"Error getting evaluations by student ID: {ex.Message}");
            }
        }
    }
}
