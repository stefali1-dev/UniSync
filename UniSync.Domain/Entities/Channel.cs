using UniSync.Domain.Common; // Assuming a similar structure for Result<T>
using System;

namespace UniSync.Domain.Entities
{
    public class Channel
    {
        public Guid ChannelId { get; private set; }
        public string ChannelName { get; private set; }
        public List<ChatUser> Users { get; set; }
        public List<Message> Messages { get; set; }

        public Channel(Guid channelId, string channelName)
        {
            ChannelId = channelId;
            ChannelName = channelName;
            Users = new List<ChatUser>();
            Messages = new List<Message>();
        }

        public void AttachUsers(List<ChatUser> users)
        {
            if (users.Count > 0)
            {
                Users.AddRange(users);
            }
        }
    }
}
