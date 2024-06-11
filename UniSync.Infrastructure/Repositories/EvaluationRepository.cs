using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using UniSync.Application.Persistence;
using UniSync.Domain.Common;
using UniSync.Domain.Entities;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Infrastructure.Repositories
{
    public class EvaluationRepository : BaseRepository<Evaluation>, IEvaluationRepository
    {
        public EvaluationRepository(UniSyncContext context) : base(context)
        {
        }

        public async Task<Result<List<Evaluation>>> GetByStudentIdAsync(Guid studentId)
        {
            try
            {
                var evaluations = await context.Evaluations
                    .Where(e => e.StudentId == studentId)
                    .ToListAsync();

                return Result<List<Evaluation>>.Success(evaluations);
            }
            catch (Exception ex)
            {
                // Log the exception
                return Result<List<Evaluation>>.Failure(ex.Message);
            }
        }
    }
}
