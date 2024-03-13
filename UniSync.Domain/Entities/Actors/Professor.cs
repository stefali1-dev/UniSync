using UniSync.Domain.Common;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Domain.Entities.Actors
{
    public class Professor
    {
        public Guid UserId { get; set; }
        public ProfessorType Type { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}