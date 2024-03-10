namespace UniSync.Application.Features.Events.Queries
{
    public class EventDto
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? Artist { get; set; }
        public DateTime EventDate { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public CategoryDto Category { get; set; }
    }
}
