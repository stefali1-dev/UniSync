using Microsoft.EntityFrameworkCore;
using UniSync.Application.Persistence;
using UniSync.Domain.Common;
using UniSync.Domain.Entities;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Infrastructure.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(UniSyncContext context) : base(context)
        {
        }

    }
}
