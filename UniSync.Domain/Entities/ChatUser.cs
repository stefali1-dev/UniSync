namespace UniSync.Domain.Entities
{
    public class ChatUser
    {
        public ChatUser(Guid chatUserId)
        {
            ChatUserId = chatUserId;
            Channels = new List<Channel>();
        }

        public Guid ChatUserId { get; set; }
        public Guid AppUserId { get; set; }
        public IList<Channel> Channels { get;}
    }
}
