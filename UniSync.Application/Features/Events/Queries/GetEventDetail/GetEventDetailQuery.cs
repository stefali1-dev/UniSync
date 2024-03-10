using MediatR;

namespace UniSync.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQuery : IRequest<GetEventDetailQueryResponse>
    {
        public Guid EventId { get; set; }
    }
}
