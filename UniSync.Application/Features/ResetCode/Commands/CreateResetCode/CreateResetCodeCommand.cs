using MediatR;

namespace UniSync.Application.Features.ResetCode.Commands.CreateResetCode
{
    public class CreateResetCodeCommand : IRequest<CreateResetCodeCommandResponse>
    {
        public string Email { get; set; }
    }
}
