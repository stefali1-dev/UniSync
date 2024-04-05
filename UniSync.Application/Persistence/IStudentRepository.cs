using UniSync.Domain.Common;
using UniSync.Domain.Entities;

namespace UniSync.Application.Persistence
{
    public interface IStudentRepository : IAsyncRepository<Student>
    {
        Task<Result<IReadOnlyList<Student>>> GetStudentsByGroupAsync(string groupName);

    }
}
