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

    }
}
