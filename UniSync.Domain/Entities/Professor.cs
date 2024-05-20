using UniSync.Domain.Common;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Domain.Entities
{
    public class Professor
    {

        public Guid ProfessorId { get; set; }
        public Guid ChatUserId { get; set; }

        public ProfessorType Type { get; set; }
        public ICollection<Course> Courses { get; set; }

    }
}