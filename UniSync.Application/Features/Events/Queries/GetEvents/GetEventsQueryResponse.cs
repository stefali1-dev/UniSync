using UniSync.Application.Responses;

namespace UniSync.Application.Features.Events.Queries.GetEvents
{
    public class GetEventsQueryResponse : BaseResponse
    {
        public GetEventsQueryResponse() : base()
        {
        }

        public List<EventDto> Events { get; set; } = default!;
    }
}
