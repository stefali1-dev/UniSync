using FluentValidation;
using UniSync.Application.Persistence;

namespace UniSync.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        private readonly IEventRepository repository;

        public CreateEventCommandValidator(IEventRepository repository)
        {
            RuleFor(e=>e.EventName)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(e=>e.Price).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than zero.");
            RuleFor(e=>e.EventDate)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(DateTime.Now)
                .WithMessage("{PropertyName} must be greater than today.");
            RuleFor(e=>e)
                .Must(EventNameAndDateUnique)
                .WithMessage("An event with the same name and date already exists.");
            RuleFor(e=>e.CategoryId).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("{PropertyName} is required.");
            this.repository = repository;
        }

        private bool EventNameAndDateUnique(CreateEventCommand command)
        {
            return !repository.IsEventNameAndDateUnique(command.EventName, command.EventDate).Result;
        }
    }
}
