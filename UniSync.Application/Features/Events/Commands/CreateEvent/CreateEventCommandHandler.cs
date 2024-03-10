using UniSync.Application.Contracts;
using UniSync.Application.Features.Events.Queries;
using UniSync.Application.Models;
using UniSync.Application.Persistence;
using UniSync.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UniSync.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, CreateEventCommandResponse>
    {
        private readonly IEventRepository eventRepository;
        private readonly IEmailService emailService;
        private readonly ILogger<CreateEventCommandHandler> logger;

        public CreateEventCommandHandler(IEventRepository eventRepository, IEmailService emailService, ILogger<CreateEventCommandHandler> logger)
        {
            this.eventRepository = eventRepository;
            this.emailService = emailService;
            this.logger = logger;
        }
        public async Task<CreateEventCommandResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(eventRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateEventCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var @event = Event.Create(request.EventName, request.Price, request.EventDate);
           
            if (@event.IsSuccess)
            {
                @event.Value.AttachCategory(request.CategoryId);

#pragma warning disable CS8604 // Possible null reference argument.
                @event.Value.AttachDescription(request.Description);
#pragma warning restore CS8604 // Possible null reference argument.

#pragma warning disable CS8604 // Possible null reference argument.
                @event.Value.AttachArtist(request.Artist);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
                @event.Value.AttachImageUrl(request.ImageUrl);
#pragma warning restore CS8604 // Possible null reference argument.
                var result = eventRepository.AddAsync(@event.Value);

                var email = new Mail
                {
                    Body = $"A new event with name:{@event.Value.EventName} and date: {@event.Value.EventDate} has been created" ,
                    // don't forget to change the email address
                    To = "olariu@gmail.com",
                    Subject = "New Event created",
                };

                try
                {
                    await emailService.SendEmailAsync(email);
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Email sending failed");
                    return new CreateEventCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { "Email sending failed" }
                    };
                }

                return new CreateEventCommandResponse
                {
                    Success = true,
                    Event = new EventDto
                    {
                        EventId = @event.Value.EventId,
                        EventName = @event.Value.EventName,
                        Price = @event.Value.Price,
                        EventDate = @event.Value.EventDate,
                        Description = @event.Value.Description,
                        Artist = @event.Value.Artist,
                        ImageUrl = @event.Value.ImageUrl,
                        Category = new CategoryDto
                        {
                            CategoryId = @event.Value.CategoryId,
                        }
                    }
                };
            }
            return new CreateEventCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { @event.Error }
            };
            
        }
    }
}
