using UniSync.Domain.Common;

namespace UniSync.Domain.Entities
{
    public class Staff : User
    {
        public Staff(Guid userId) : base(userId)
        {
        }
        public string Title { get; set; }
    }
}