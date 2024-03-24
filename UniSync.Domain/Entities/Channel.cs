using UniSync.Domain.Common; // Assuming a similar structure for Result<T>
using System;

namespace UniSync.Domain.Entities
{
    public class Channel
    {
        public Guid ChannelId { get; private set; }
        public string ChannelName { get; private set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Message> Messages { get; set; }


    }
}
