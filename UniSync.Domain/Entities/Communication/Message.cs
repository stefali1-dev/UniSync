namespace UniSync.Domain.Entities.Communication
{
    public class Message
    {
        public Guid MessageId { get; private set; }
        public DateTime MessageDate { get; private set; }
        public Guid SenderId { get; private set; }
        public Guid? ReceiverId { get; private set; } // null if it's a channel message
        public Guid? ChannelId { get; private set; } // null if it's a direct message

        protected Message(Guid messageId, DateTime messageDate, Guid senderId, Guid? receiverId, Guid? channelId)
        {
            MessageId = messageId;
            MessageDate = messageDate;
            SenderId = senderId;
            ReceiverId = receiverId;
            ChannelId = channelId;
        }

    }
}
