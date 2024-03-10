using UniSync.Application.Features.Events.Queries;
using UniSync.Application.Responses;

namespace UniSync.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandResponse : BaseResponse
    {
        public CreateEventCommandResponse() : base()
        {
        }

        public EventDto Event { get; set; } = default!;
    }
}