using UniSync.Domain.Entities;

namespace UniSync.Application.Features.Channels
{
    public class ChannelCreationDto
    {
        public string ChannelName { get; set; }
        public List<string> ChatUsersIds { get; set; }

    }
}
