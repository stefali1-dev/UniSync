using FluentValidation;

namespace UniSync.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .NotNull().WithMessage("Id is required");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required")
                .NotNull().WithMessage("FirstName is required");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required")
                .NotNull().WithMessage("LastName is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .NotNull().WithMessage("Email is required");

            RuleFor(x => x.Bio)
                .MaximumLength(2000);
        }
    }
}
