namespace UniSync.Domain.Entities.Actors
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
