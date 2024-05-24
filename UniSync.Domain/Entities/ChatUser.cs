namespace UniSync.Domain.Entities
{
    public class ChatUser
    {
        public ChatUser(Guid chatUserId, Guid appUserId)
        {
            ChatUserId = chatUserId;
            AppUserId = appUserId;
        }

        public Guid ChatUserId { get; set; }
        public Guid AppUserId { get; set; }

        //navigation properties
        public virtual ICollection<Channel> Channels { get; set; }

        public void AttachChannel(Channel channel)
        {
            if(Channels == null)
            {
                Channels = new List<Channel>();
            }
            Channels.Add(channel);
        }

        
    }
}
