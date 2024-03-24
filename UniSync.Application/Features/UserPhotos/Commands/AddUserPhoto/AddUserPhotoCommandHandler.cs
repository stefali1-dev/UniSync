using UniSync.Application.Features.Users;
using UniSync.Application.Persistence;
using UniSync.Domain.Entities;
using MediatR;
using Ergo.Domain.Entities;

namespace UniSync.Application.Features.UserPhotos.Commands.AddUserPhoto
{
    public class AddUserPhotoCommandHandler : IRequestHandler<AddUserPhotoCommand, AddUserPhotoCommandResponse>
    {
        private readonly IUserPhotoRepository userPhotoRepository;

        public AddUserPhotoCommandHandler(IUserPhotoRepository userPhotoRepository)
        {
            this.userPhotoRepository = userPhotoRepository;
        }

        public Task<AddUserPhotoCommandResponse> Handle(AddUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddUserPhotoCommandValidator();
            var validatorResult = validator.Validate(request);
            if (!validatorResult.IsValid)
            {
                return Task.FromResult(new AddUserPhotoCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                });
            }

            var userPhoto = UserPhoto.Create(request.PhotoUrl, request.UserId);
            if (!userPhoto.IsSuccess)
            {
                return Task.FromResult(new AddUserPhotoCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { userPhoto.Error }
                });
            }

            userPhotoRepository.AddAsync(userPhoto.Value);
            return Task.FromResult(new AddUserPhotoCommandResponse
            {
                Success = true,
                UserPhoto = new UserCloudPhotoDto
                {
                    UserPhotoId = userPhoto.Value.UserPhotoId,
                    PhotoUrl = userPhoto.Value.PhotoUrl
                }
            });
        }
    }
}
