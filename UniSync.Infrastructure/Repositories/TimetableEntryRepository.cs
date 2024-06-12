using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using UniSync.Application.Persistence;
using UniSync.Domain.Common;
using UniSync.Domain.Entities;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Infrastructure.Repositories
{
    public class TimetableEntryRepository : BaseRepository<TimetableEntry>, ITimetableEntryRepository
    {
        public TimetableEntryRepository(UniSyncContext context) : base(context)
        {
        }

        public async Task<List<TimetableEntry>> GetByProfessorIdAsync(Guid professorId)
        {
            return await context.TimetableEntries
                .Where(te => te.ProfessorId == professorId)
                .ToListAsync();
        }
    }
}
