using UniSync.Domain.Common; // Assuming a similar structure for Result<T>
using System;

namespace UniSync.Domain.Entities
{
    public class Channel
    {
        public Channel(Guid channelId, string channelName)
        {
            ChannelId = channelId;
            ChannelName = channelName;
            Messages = new List<Message>();
        }
        public Guid ChannelId { get; private set; }
        public string ChannelName { get; private set; }

        //navigation properties
        public virtual ICollection<ChatUser> Users { get; set; }
        public virtual ICollection<Message> Messages { get; set; }


        public void AttachUsers(ICollection<ChatUser> users)
        {
            if(Users == null)
            {
                Users = new List<ChatUser>();
            }
            
            foreach(ChatUser user in users)
            {
                Users.Add(user);

            }

        }

        public void AttachMessage(Message message)
        {
            if (Messages == null)
            {
                Messages = new List<Message>();
            }
            Messages.Add(message);
        }
    }
}
