using UniSync.Domain.Common;

namespace UniSync.Domain.Entities.Actors
{
    public class Professor : User
    {
        public Professor(Guid userId, string firstName, string lastName, string position, DateTime hireDate)
            : base(userId, firstName, lastName)
        {
            Position = position;
            HireDate = hireDate;
        }

        public string Position { get; private set; }
        public DateTime HireDate { get; private set; }
        public static Result<Professor> Create(Guid userId, string firstName, string lastName, string position, DateTime hireDate)
        {
            if (userId == Guid.Empty)
            {
                return Result<Professor>.Failure("UserId cannot be empty");
            }

            return Result<Professor>.Success(new Professor(userId, firstName, lastName, position, hireDate));
        }


    }
}