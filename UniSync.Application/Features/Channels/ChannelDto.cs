using UniSync.Domain.Entities;

namespace UniSync.Application.Features.Channels
{
    public class ChannelDto
    {
        public Guid ChannelId { get; set; }
        public string ChannelName { get; set; }
        public ICollection<ChatUser> Users { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}
