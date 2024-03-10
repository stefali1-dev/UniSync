using UniSync.Application.Persistence;
using MediatR;

namespace UniSync.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, UpdateEventViewModel>
    {
        private readonly IEventRepository eventRepository;

        public UpdateEventCommandHandler(IEventRepository eventRepository )
        {
            this.eventRepository = eventRepository;
        }

        public async Task<UpdateEventViewModel> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEventCommandValidator(eventRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                return new UpdateEventViewModel
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var @event = await eventRepository.FindByIdAsync(request.EventId);
            if (@event == null)
            {
                return new UpdateEventViewModel
                {
                    Success = false,
                    ValidationsErrors = ["Event not found"]
                };
            }
            @event.Value.Update(request.EventName, request.Price, request.EventDate, request.Artist, request.Description, request.ImageUrl, request.CategoryId);
            return new UpdateEventViewModel 
            { 
                Success = true, 
                EventId = @event.Value.EventId,
                EventName = @event.Value.EventName,
                Price = @event.Value.Price,
                EventDate = @event.Value.EventDate,
                Artist = @event.Value.Artist,
                Description = @event.Value.Description,
                ImageUrl = @event.Value.ImageUrl,
                CategoryId = @event.Value.CategoryId
            };
        }
    }
}
