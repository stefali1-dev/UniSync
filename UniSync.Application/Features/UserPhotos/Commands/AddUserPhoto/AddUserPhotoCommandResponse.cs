using UniSync.Application.Features.Users;
using UniSync.Application.Responses;

namespace UniSync.Application.Features.UserPhotos.Commands.AddUserPhoto
{
    public class AddUserPhotoCommandResponse : BaseResponse
    {
        public AddUserPhotoCommandResponse() : base()
        {
        }

        public UserCloudPhotoDto UserPhoto { get; set; }
    }
}