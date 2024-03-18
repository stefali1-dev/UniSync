using UniSync.Domain.Common;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Domain.Entities
{
    public class Professor : User
    {
        public Professor(Guid userId) : base(userId)
        {
        }
        public ProfessorType Type { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}