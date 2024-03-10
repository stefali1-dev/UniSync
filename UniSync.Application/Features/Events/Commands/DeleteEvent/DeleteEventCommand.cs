using MediatR;

namespace UniSync.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest<DeleteEventCommandResponse>
    {
        public Guid EventId { get; set; }
    }
}
