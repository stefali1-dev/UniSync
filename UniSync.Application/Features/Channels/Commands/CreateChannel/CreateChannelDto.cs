using UniSync.Domain.Entities;

namespace UniSync.Application.Features.Channels.Commands.CreateChannel
{
    public class CreateChannelDto
    {
        public string ChannelName { get; private set; }
        public ICollection<string> UserIds { get; set; }
    }
}