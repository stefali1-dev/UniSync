namespace UniSync.Domain.Entities
{
    public class Message
    {
        public Guid MessageId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        //public Guid UserId { get; set; }
        public string? SenderName { get; set; }
        //public Guid? ChannelId { get; set; }
        public string? ChannelName { get; set; }
    }
}
