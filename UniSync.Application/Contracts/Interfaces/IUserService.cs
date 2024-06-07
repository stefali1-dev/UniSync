using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Application.Features.Courses;
using UniSync.Application.Features.Users;
using UniSync.Application.Features.Users.Queries;

namespace UniSync.Application.Contracts.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserSearchDto>> GetAllStudents();
        public Task<List<UserSearchDto>> GetAllProfessors();
        public Task<List<UserSearchDto>> SearchStudents(string searchValue);
        public Task<List<UserSearchDto>> SearchProfessors(string searchValue);


    }
}
