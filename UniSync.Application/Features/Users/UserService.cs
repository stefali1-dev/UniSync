using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Persistence;
using UniSync.Domain.Common;

namespace UniSync.Application.Features.Users
{
    public class UserService : IUserService
    {
        private readonly IUserManager userManager;

        public UserService(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public async Task<List<UserSearchDto>> GetAllProfessors()
        {
            var allUsers = await userManager.GetAllAsync();
            if (!allUsers.IsSuccess)
                return new List<UserSearchDto>();

            return allUsers.Value
                .Where(u => u.Roles.Contains("Professor"))
                .Select(u => new UserSearchDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    UserPhoto = u.UserPhoto
                })
                .ToList();
        }

        public async Task<List<UserSearchDto>> GetAllStudents()
        {
            var allUsers = await userManager.GetAllAsync();
            if (!allUsers.IsSuccess)
                return new List<UserSearchDto>();

            return allUsers.Value
                .Where(u => u.Roles.Contains("Student"))
                .Select(u => new UserSearchDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    UserPhoto = u.UserPhoto
                })
                .ToList();
        }

        public async Task<List<UserSearchDto>> SearchProfessors(string searchValue)
        {
            var allUsers = await userManager.GetAllAsync();
            if (!allUsers.IsSuccess)
                return new List<UserSearchDto>();

            var users = allUsers.Value
                .Where(u => u.Roles.Contains("Professor"))
                .Where(u =>
                    (!string.IsNullOrWhiteSpace(u.FirstName) && u.FirstName.ToLower().Contains(searchValue.ToLower())) ||
                    (!string.IsNullOrWhiteSpace(u.LastName) && u.LastName.ToLower().Contains(searchValue.ToLower()))
                )
                .Select(u => new UserSearchDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    UserPhoto = u.UserPhoto
                })
                .ToList();

            return users;
        }

        public async Task<List<UserSearchDto>> SearchStudents(string searchValue)
        {
            var allUsers = await userManager.GetAllAsync();
            if (!allUsers.IsSuccess)
                return new List<UserSearchDto>();

            var users = allUsers.Value
                .Where(u => u.Roles.Contains("Student"))
                .Where(u =>
                    (!string.IsNullOrWhiteSpace(u.FirstName) && u.FirstName.ToLower().Contains(searchValue.ToLower())) ||
                    (!string.IsNullOrWhiteSpace(u.LastName) && u.LastName.ToLower().Contains(searchValue.ToLower()))
                )
                .Select(u => new UserSearchDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    UserPhoto = u.UserPhoto
                })
                .ToList();

            return users;
        }
    }
}