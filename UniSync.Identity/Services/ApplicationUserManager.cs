using UniSync.Application.Features.Users;
using UniSync.Application.Persistence;
using UniSync.Domain.Common;
using UniSync.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace UniSync.Identity.Services
{
    public class ApplicationUserManager : IUserManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserPhotoRepository userPhotoRepository;

        public ApplicationUserManager(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserPhotoRepository userPhotoRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userPhotoRepository = userPhotoRepository;
        }

        public async Task<Result<UserDto>> FindByIdAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result<UserDto>.Failure($"User with id {userId} not found");
            }

            var userDtos = MapToUserDto(user);
            var roles = await userManager.GetRolesAsync(user);
            userDtos.Roles = roles.ToList();

            //var userPhoto = await userPhotoRepository.GetUserPhotoByUserIdAsync(user.Id);
            //if (userPhoto.IsSuccess)
            //{
            //    userDtos.UserPhoto = new UserCloudPhotoDto
            //    {
            //        UserPhotoId = userPhoto.Value.UserPhotoId,
            //        PhotoUrl = userPhoto.Value.PhotoUrl
            //    };
            //}

            return Result<UserDto>.Success(userDtos);
        }

        public async Task<Result<UserDto>> FindByUsernameAsync(string username)
        {

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
                return Result<UserDto>.Failure($"User with username {username} not found");
            var userDtos = MapToUserDto(user);
            var roles = await userManager.GetRolesAsync(user);
            userDtos.Roles = roles.ToList();
            return Result<UserDto>.Success(userDtos);
        }

        public async Task<Result<UserDto>> FindByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<UserDto>.Failure($"User with email {email} not found");
            var userDtos = MapToUserDto(user);
            var roles = await userManager.GetRolesAsync(user);
            userDtos.Roles = roles.ToList();
            return Result<UserDto>.Success(userDtos);
        }

        public async Task<Result<List<UserDto>>> GetAllAsync()
        {
            var users = userManager.Users.ToList();
            var userDtos = users.Select(u => MapToUserDto(u)).ToList();

            foreach (var userDto in userDtos)
            {
                var appUser = await userManager.FindByIdAsync(userDto.UserId.ToString());
                if (appUser != null)
                {
                    var roles = await userManager.GetRolesAsync(appUser);
                    userDto.Roles = roles.ToList();
                }
            }

            return Result<List<UserDto>>.Success(userDtos);
        }


        public async Task<Result<UserDto>> DeleteAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return Result<UserDto>.Failure($"User with id {userId} not found");

            var deleteResult = await userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
                return Result<UserDto>.Failure($"User with id {userId} not deleted");

            return Result<UserDto>.Success(MapToUserDto(user));
        }


        public async Task<Result<UserDto>> UpdateAsync(UserDto userDto)
        {
            var userToUpdate = await userManager.FindByIdAsync(userDto.UserId.ToString());
            if (userToUpdate == null)
                return Result<UserDto>.Failure($"User with id {userDto.UserId} not found");

            UpdateUserProperties(userToUpdate, userDto);

            var updateResult = await userManager.UpdateAsync(userToUpdate);
            return updateResult.Succeeded ? Result<UserDto>.Success(MapToUserDto(userToUpdate)) : Result<UserDto>.Failure($"User with id {userDto.UserId} not updated");
        }

        public async Task<Result<UserDto>> UpdateRoleAsync(UserDto userDto, string role)
        {
            var userToUpdate = await userManager.FindByIdAsync(userDto.UserId.ToString());
            if (userToUpdate == null)
                return Result<UserDto>.Failure($"User with id {userDto.UserId} not found");

            if (role != "Admin" && role != "User")
                return Result<UserDto>.Failure($"Role {role} not found");

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            if (await userManager.IsInRoleAsync(userToUpdate, role))
                return Result<UserDto>.Failure($"User with id {userDto.UserId} already has role {role}");

            var addToRoleResult = await userManager.AddToRoleAsync(userToUpdate, role);
            return addToRoleResult.Succeeded ? Result<UserDto>.Success(MapToUserDto(userToUpdate)) : Result<UserDto>.Failure($"User with id {userDto.UserId} not updated");
        }

        public async Task<Result<List<string>>> GetUserRolesAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result<List<string>>.Failure($"User with id {userId} not found");
            }

            var roles = await userManager.GetRolesAsync(user);
            return Result<List<string>>.Success(roles.ToList());
        }
        private void UpdateUserProperties(ApplicationUser user, UserDto userDto)
        {
            user.Facebook = userDto.Social?.Facebook;
            user.Instagram = userDto.Social?.Instagram;
            user.GitHub = userDto.Social?.GitHub;
            user.LinkedIn = userDto.Social?.LinkedIn;
        }
        private UserDto MapToUserDto(ApplicationUser user)
        {
            return new UserDto
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Bio = user.Bio,
                UserPhoto = user.UserPhoto,
                Social = new Social
                {
                    Facebook = user.Facebook,
                    Instagram = user.Instagram,
                    GitHub = user.GitHub,
                    LinkedIn = user.LinkedIn
                }
            };
        }


    }
}
