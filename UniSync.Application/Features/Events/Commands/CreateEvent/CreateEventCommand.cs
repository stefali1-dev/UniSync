using MediatR;

namespace UniSync.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest<CreateEventCommandResponse>
    {
        public string EventName { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? Artist { get; set; }
        public DateTime EventDate { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
    }
}
