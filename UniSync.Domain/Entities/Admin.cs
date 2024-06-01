using UniSync.Domain.Common;

namespace UniSync.Domain.Entities
{
    public class Admin
    {
        public Guid AdminId { get; set; }
        public string? Title { get; set; }
    }
}