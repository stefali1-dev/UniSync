using MediatR;
using UniSync.Application.Persistence;

namespace UniSync.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, GetEventDetailQueryResponse>
    {
        private readonly IEventRepository repository;

        public GetEventDetailQueryHandler(IEventRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetEventDetailQueryResponse> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
			var @event = await repository.FindByIdAsync(request.EventId);
            if (!@event.IsSuccess)
            {
                return new GetEventDetailQueryResponse
                {
                    Success = false,
                    ValidationsErrors = [@event.Error]
                };
            }

            return new GetEventDetailQueryResponse
            {
                Success = true,
                Event = new EventDto
                {
                    EventId = @event.Value.EventId,
                    EventName = @event.Value.EventName,
                    Price = @event.Value.Price,
                    EventDate = @event.Value.EventDate,
                    Artist = @event.Value.Artist,
                    Description = @event.Value.Description,
                    ImageUrl = @event.Value.ImageUrl,
                    Category = new CategoryDto 
                    { 
                        CategoryId = @event.Value.CategoryId, 
                        CategoryName = @event.Value.Category.CategoryName
                    }
                }
            };
        }
    }
}
