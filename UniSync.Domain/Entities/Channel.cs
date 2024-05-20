using UniSync.Domain.Common; // Assuming a similar structure for Result<T>
using System;

namespace UniSync.Domain.Entities
{
    public class Channel
    {
        public Guid ChannelId { get; private set; }
        public string ChannelName { get; private set; }
        public ICollection<ChatUser> Users { get; set; }
        public ICollection<Message> Messages { get; set; }

        public Channel(Guid channelId, string channelName, ICollection<ChatUser> users, ICollection<Message> messages)
        {
            ChannelId = channelId;
            ChannelName = channelName;
            Users = users;
            Messages = messages;
        }
    }
}
