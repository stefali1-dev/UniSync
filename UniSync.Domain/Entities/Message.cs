namespace UniSync.Domain.Entities
{
    public class Message
    {
        //public Message(Guid messageId, string content, Guid chatUserId, DateTime timestamp, Channel channel, ChatUser chatUser)
        //{
        //    MessageId = messageId;
        //    Content = content;
        //    ChatUserId = chatUserId;
        //    Timestamp = timestamp;
        //    Channel = channel;
        //    ChatUser = chatUser;
        //}

        public Guid MessageId { get; set; }
        public string Content { get; set; }
        public Guid? ReceiverId { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid ChatUserId { get; set; }
        public Guid ChannelId { get; set; }

        //navigation properties
        public virtual Channel Channel { get; set; }
        public virtual ChatUser ChatUser { get; set; }
        
    }
}
