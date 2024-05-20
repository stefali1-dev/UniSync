using MediatR;
using UniSync.Domain.Entities;

namespace UniSync.Application.Features.Channels.Commands.CreateChannel
{
    public class CreateChannelCommand : IRequest<CreateChannelCommandResponse>
    {
        public string ChannelName { get; private set; }
        // TODO: Finish channel creation
        public ICollection<string> UserIds { get; set; }
    }
}
