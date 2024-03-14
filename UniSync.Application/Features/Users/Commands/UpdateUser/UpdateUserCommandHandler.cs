using UniSync.Application.Features.Users.Queries;
using UniSync.Application.Persistence;
using MediatR;

namespace UniSync.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
    {
        private readonly IUserManager userRepository;
        private readonly IUserPhotoRepository userPhotoRepository;

        public UpdateUserCommandHandler(IUserManager userRepository, IUserPhotoRepository userPhotoRepository)
        {
            this.userRepository = userRepository;
            this.userPhotoRepository = userPhotoRepository;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateUserCommandResponse();

            var user = await userRepository.FindByIdAsync(request.Id);
            if(!user.IsSuccess)
            {
                return new UpdateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "User with id this does not exists" }
                };
            }

            request.FirstName ??= user.Value.FirstName;
            request.LastName ??= user.Value.LastName;
            request.Email ??= user.Value.Email;
            request.Bio ??= user.Value.Bio;
            request.Social ??= user.Value.Social;

            var validator = new UpdateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return new UpdateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var userByEmail = await userRepository.FindByEmailAsync(request.Email);
            if (userByEmail.IsSuccess && userByEmail.Value.UserId != user.Value.UserId)
            {
                Console.WriteLine("Email" + request.Email);

                return new UpdateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Email already exists" }
                };
            }

            var userPhoto = await userPhotoRepository.GetUserPhotoByUserIdAsync(request.Id.ToString());
            
            UserDto userDto = new()
            {
                UserId = user.Value.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Bio = request.Bio,
                Social = request.Social
            };

            var result = await userRepository.UpdateAsync(userDto);
            if (!result.IsSuccess)
            {
                return new UpdateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }

            return new UpdateUserCommandResponse
            {
                Success = true,
                User = new UpdateUserDto
                {
                    FirstName = result.Value.FirstName,
                    LastName = result.Value.LastName,
                    UserPhoto = userPhoto.IsSuccess ? new UserCloudPhotoDto
                    {
                        UserPhotoId = userPhoto.Value.UserPhotoId,
                        PhotoUrl = userPhoto.Value.PhotoUrl
                    } : null,
                    Email = result.Value.Email,
                    Bio = result.Value.Bio,
                    Social = result.Value.Social
                }
            };
        }
    }
}
