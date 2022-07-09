using FluentValidation;

namespace LintCoder.Identity.API.Application.Commands.Account.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("登录名不能为空").MaximumLength(100);

            RuleFor(x => x.Password).NotEmpty().MaximumLength(100);
        }
    }
}
