

using UniSync.Domain.Common;
using UniSync.Domain.Entities;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Application.Persistence
{
    public interface ICourseRepository : IAsyncRepository<Course>
    {
        Task<Result<Course>> FindByCourseNumberAsync(string courseNumber);

    }
}
