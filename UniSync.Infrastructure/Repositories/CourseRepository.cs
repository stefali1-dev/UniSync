using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
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

        public async Task<Result<Course>> FindByCourseNumberAsync(string courseNumber)
        {
            try
            {
                var course = await context.Courses
                                           .FirstOrDefaultAsync(c => c.CourseNumber == courseNumber);

                if (course != null)
                {
                    return Result<Course>.Success(course);
                }
                else
                {
                    return Result<Course>.Failure("Course not found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                return Result<Course>.Failure($"An error occurred while retrieving the course: {ex.Message}");
            }
        }

    }
}
