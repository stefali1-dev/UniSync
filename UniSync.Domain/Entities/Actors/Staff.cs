using UniSync.Domain.Common;

namespace UniSync.Domain.Entities.Actors
{
    public class Staff : User
    {
        public Staff(Guid userId, string firstName, string lastName, string position, DateTime hireDate)
            : base(userId, firstName, lastName)
        {
            Position = position;
            HireDate = hireDate;
        }

        public string Position { get; private set; }
        public DateTime HireDate { get; private set; }

        public static Result<Staff> Create(Guid userId, string firstName, string lastName, string position, DateTime hireDate)
        {
            if (userId == Guid.Empty)
            {
                return Result<Staff>.Failure("UserId cannot be empty");
            }

            return Result<Staff>.Success(new Staff(userId, firstName, lastName, position, hireDate));
        }
    }
}