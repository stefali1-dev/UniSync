

using UniSync.Domain.Common;
using UniSync.Domain.Entities;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Application.Persistence
{
    public interface ITimetableEntryRepository : IAsyncRepository<TimetableEntry>
    {
        public Task<List<TimetableEntry>> GetByProfessorIdAsync(Guid professorId);
        public Task<List<TimetableEntry>> GetByGroupNameAsync(string groupName);

    }
}
