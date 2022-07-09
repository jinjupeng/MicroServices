using FluentValidation;

namespace LintCoder.Identity.API.Application.Commands.User.CreateUser
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
        }
    }
}
