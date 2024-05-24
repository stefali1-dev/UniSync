using MediatR;
using UniSync.Domain.Entities;

namespace UniSync.Application.Features.Channels.Commands.CreateChannel
{
    public class CreateChannelCommand : IRequest<CreateChannelCommandResponse>
    {
        public string ChannelName { get; set; }
        public List<string> ChatUserIds { get; set; }
    }
}
