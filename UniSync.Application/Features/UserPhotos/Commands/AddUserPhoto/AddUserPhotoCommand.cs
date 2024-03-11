using MediatR;
using Microsoft.AspNetCore.Http;

namespace UniSync.Application.Features.UserPhotos.Commands.AddUserPhoto
{
    public class AddUserPhotoCommand : IRequest<AddUserPhotoCommandResponse>
    {
        public IFormFile File { get; set; }
        public string UserId { get; set; }
        public string PhotoUrl { get; set; }
    }
}
