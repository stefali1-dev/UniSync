using FluentValidation;
using UniSync.Application.Persistence;

namespace UniSync.Application.Features.Channels.Commands.CreateChannel
{
    public class CreateChannelCommandValidator : AbstractValidator<CreateChannelCommand>
    {
        private readonly IChannelRepository repository;

        public CreateChannelCommandValidator(IChannelRepository repository)
        {
            RuleFor(e=>e.ChannelName)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50)
                .WithMessage("{PropertyName} must not exceed 50 characters.");
            this.repository = repository;
        }

    }
}
