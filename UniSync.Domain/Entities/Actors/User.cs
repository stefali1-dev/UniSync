
public class User
{
    protected User(Guid userId, string firstName, string lastName)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? Email { get; private set; }
    public string? PhotoCloudUrl { get; private set; }
}


