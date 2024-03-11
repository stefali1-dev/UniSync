using UniSync.Application.Responses;

namespace UniSync.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandResponse : BaseResponse
    {
        public UpdateUserCommandResponse() : base()
        {
        }

        public UpdateUserDto User { get; set; }
    }
}