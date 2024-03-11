
using UniSync.Domain.Common;

public class User
{
    protected User(Guid userId, string FirstName, string LastName)
    {
        UserId = userId;
        this.FirstName = FirstName;
        this.LastName = LastName;
    }

    public Guid UserId { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Email { get; private set; }
    public string? PhotoCloudUrl { get; private set; }

    public static Result<User> Create(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return Result<User>.Failure("UserId cannot be empty");
        }

        return Result<User>.Success(new User(userId, "", ""));
    }
}


