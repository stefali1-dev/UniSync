using UniSync.Application.Responses;

namespace UniSync.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryResponse : BaseResponse
    {
        public GetEventDetailQueryResponse() : base()
        {
        }

        public EventDto Event { get; set; } = default!;
    }
}
