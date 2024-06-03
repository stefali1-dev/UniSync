using UniSync.Application.Features.Courses;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Application.Contracts.Interfaces
{
    public interface ICoursesService
    {
        public Task LoadCoursesFromCsv(string csvPath);
        public Task<List<CourseDto>> GetCoursesByProfessorId(string professorId);
        public Task<List<CourseDto>> GetCoursesByStudentId(string studentId);

    }
}
