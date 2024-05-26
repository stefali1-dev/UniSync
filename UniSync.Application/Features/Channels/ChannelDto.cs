using UniSync.Domain.Entities;

namespace UniSync.Application.Features.Channels
{
    public class ChannelDto
    {
        public Guid ChannelId { get; set; }
        public string ChannelName { get; set; }
        public List<Guid> ChatUsersIds { get; set; }
        public List<Guid>? MessagesIds { get; set; }

    }
}
