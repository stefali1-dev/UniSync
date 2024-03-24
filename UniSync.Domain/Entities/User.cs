namespace UniSync.Domain.Entities
{
    public class User
    {
        public User(Guid userId)
        {
            UserId = userId;
            Messages = new List<Message>();
            Channels = new List<Channel>();
        }

        public Guid UserId { get; set; }
        public IList<Message> Messages { get; set; }
        public IList<Channel> Channels { get;}
    }
}
