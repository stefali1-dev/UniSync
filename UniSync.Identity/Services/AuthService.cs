using UniSync.Application.Contracts.Identity;
using UniSync.Application.Models.Identity;
using UniSync.Application.Persistence;
using UniSync.Identity.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using UniSync.Domain.Entities;
using UniSync.Application.Contracts.Interfaces;
using System;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IStudentRepository studentRepository;
        private readonly IChatUserRepository chatUserRepository;
        private readonly IProfessorRepository professorRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly IPasswordResetCode passwordResetCodeRepository;
        private readonly IRoleAssignmentService roleAssignmentService;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager, IStudentRepository studentRepository, IPasswordResetCode passwordResetCodeRepository, IRoleAssignmentService roleAssignmentService, IChatUserRepository chatUserRepository, IProfessorRepository professorRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.studentRepository = studentRepository;
            this.passwordResetCodeRepository = passwordResetCodeRepository;
            this.roleAssignmentService = roleAssignmentService;
            this.chatUserRepository = chatUserRepository;
            this.professorRepository = professorRepository;
        }
        public async Task<(int, string)> Registeration(RegistrationModel model, string role)
        {
            var userExistsByEmail = await userManager.FindByEmailAsync(model.Email);
            if (userExistsByEmail != null)
                return (0, "User with this email already exists");
            if (!IsPasswordValid(model.Password))
                return (0, "Password is not valid! The password must have at least 7 characters and needs to include a capital letter, a symbol, a digit.");
            
            var userInfo = roleAssignmentService.GetUserInfoByRegistrationId(model.RegistrationId);
            string randomString = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 8).Select(s => s[new Random().Next(s.Length)]).ToArray());
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                UserName = randomString
            };

            var createUserResult = await userManager.CreateAsync(user, model.Password);


            if (!createUserResult.Succeeded)
            {
                return (0, "User creation failed! Please check user details and try again.");
            }

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            if (await roleManager.RoleExistsAsync(UserRoles.User))
                await userManager.AddToRoleAsync(user, role);

            // TODO
            //var userDomain = User.Create(Guid.Parse(user.Id));
            //await userRepository.AddAsync(userDomain.Value);

            //var _user = CreateUserSubclass(model.RegistrationId);
            

            var chatUser = new ChatUser(Guid.Parse(user.Id), Guid.NewGuid());
            await chatUserRepository.AddAsync(chatUser);

            var registrationId = model.RegistrationId;

            // TODO: implement real logic
            if(registrationId.StartsWith("3"))
            {
                var student = new Student
                {
                    StudentId = Guid.NewGuid(),
                    ChatUserId = chatUser.ChatUserId,
                    Semester = Convert.ToInt32(userInfo.Semester),
                    Group = userInfo.Group
                };
                
                await studentRepository.AddAsync(student);
            }

            if (registrationId.StartsWith("2"))
            {
                var professor = new Professor
                {
                    ProfessorId = Guid.NewGuid(),
                    ChatUserId = chatUser.ChatUserId,
                    Type = Domain.Common.ProfessorType.Course,
                    Courses = new List<Course>()

                };
                await professorRepository.AddAsync(professor);
            }

            return (1, "User created successfully!");
        }

        public async Task<(int, string)> Login(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return (0, "Invalid email");
            if (!await userManager.CheckPasswordAsync(user, model.Password!))
                return (0, "Invalid password");

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Email, user.Email!),
               new Claim(ClaimTypes.NameIdentifier, user.Id!),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string token = GenerateToken(authClaims);
            return (1, token);
        }
        public async Task<(int, string)> LoginWithGoogle(string googleToken)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(googleToken);
            }
            catch (Exception)
            {
                return (0, "Invalid Google Token");
            }

            var user = await userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                var usernameToSearch = payload.Email.Split("@")[0];
                var userName = userManager.Users.FirstOrDefault(u => u.UserName == usernameToSearch);
                var randomGenerator = RandomNumberGenerator.Create();
                if (userName != null)
                {
                    usernameToSearch += randomGenerator.GetHashCode().ToString()[..3];
                }
                user = new ApplicationUser
                {
                    UserName = usernameToSearch,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    Email = payload.Email
                };

                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return (0, "Failed to create user");
                }
                // temorarily leave out LoginWithGoogle

                //var userDomain = User.Create(Guid.Parse(user.Id));
                //await userRepository.AddAsync(userDomain.Value);

                //var _user = CreateUserSubclass(model.RegistrationId);
                //await userRepository.AddAsync(_user);

                await userManager.AddToRoleAsync(user, "User");
            }
            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName!),
               new Claim(ClaimTypes.Email, user.Email!),
               new Claim(ClaimTypes.NameIdentifier, user.Id!),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateToken(authClaims);

            return (1, token);
        }
        public async Task<(int, string)> ResetPassword(ResetPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return (0, "User with the provided email does not exist.");
            var resetCodeValid = await passwordResetCodeRepository.HasValidCodeByEmailAsync(model.Email, model.Code);
            if (!resetCodeValid)
                return (0, "Invalid reset code.");
            var codeHash = userManager.PasswordHasher.HashPassword(user, model.Password);
            user.PasswordHash = codeHash;
            var updateResult = await userManager.UpdateAsync(user);
            await passwordResetCodeRepository.InvalidateExistingCodesAsync(model.Email);

            if (!updateResult.Succeeded)
            {
                return (0, "Password reset failed! Please check user details and try again.");
            }

            await userManager.UpdateSecurityStampAsync(user);

            return (1, "Password reset successfully!");

        }

        public async Task<(int, string)> Logout()
        {
            await signInManager.SignOutAsync();
            return (1, "User logged out successfully!");
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["JWT:ValidIssuer"]!,
                Audience = configuration["JWT:ValidAudience"]!,
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private bool IsPasswordValid(string password)
        {
            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var result = passwordValidator.ValidateAsync(userManager, null, password);
            return result.Result.Succeeded;
        }

        //private User CreateUserSubclass(string registrationId)
        //{
        //    var userRole = roleAssignmentService.GetUserInfoByRegistrationId(registrationId);
        //    return userRole switch
        //    {
        //        UserRoles.Student => new Student(Guid.NewGuid()),
        //        UserRoles.Professor => new Professor(Guid.NewGuid()),
        //        UserRoles.Staff => new Staff(Guid.NewGuid()),
        //        _ => throw new Exception("Invalid Registration ID")
        //    };

        //}




    }
}
