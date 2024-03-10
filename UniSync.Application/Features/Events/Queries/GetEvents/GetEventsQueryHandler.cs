using UniSync.Application.Persistence;
using MediatR;

namespace UniSync.Application.Features.Events.Queries.GetEvents
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, GetEventsQueryResponse>
    {
        private readonly IEventRepository repository;

        public GetEventsQueryHandler(IEventRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetEventsQueryResponse> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
			var result = await repository.GetAllAsync();
            var events = result.Value.Select(e => new EventDto
            {
                EventId = e.EventId,
                EventName = e.EventName,
                Price = e.Price,
                EventDate = e.EventDate,
                Artist = e.Artist,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                Category = new CategoryDto
                {
                    CategoryId = e.Category.CategoryId,
                    CategoryName = e.Category.CategoryName
                }
            }).ToList();
            return new GetEventsQueryResponse 
            { 
                Events = events, 
                Success = true 
            };
        }
    }
}
