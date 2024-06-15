using UniSync.Domain.Common; // Assuming a similar structure for Result<T>
using System;

namespace UniSync.Domain.Entities
{
    public class Channel
    {
        public Guid ChannelId { get; set; }
        public string ChannelName { get; set; }

        //navigation properties
        public virtual List<ChatUser> Users { get; set; }
        public virtual List<Message> Messages { get; set; }


        public void AttachUser(ChatUser user)
        {
            if (Users == null)
            {
                Users = new List<ChatUser>();
            }
            Users.Add(user);

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
