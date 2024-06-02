using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Application.Contracts.Interfaces
{
    public interface ICoursesService
    {
        public Task LoadCoursesFromCsv(string csvPath);
        public Task<List<Course>> GetCoursesByProfessorId(string professorId);
        public Task<List<Course>> GetCoursesByStudentId(string studentId);

    }
}
