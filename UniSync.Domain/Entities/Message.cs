namespace UniSync.Domain.Entities
{
    public class Message
    {
        public Guid MessageId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public User Sender { get; set; }
        public int ChannelName { get; set; }
        public Channel Channel { get; set; }
    }
}
