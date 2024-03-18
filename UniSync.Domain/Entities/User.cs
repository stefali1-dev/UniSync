namespace UniSync.Domain.Entities
{
    public class User
    {
        public User(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
