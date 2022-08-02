using FluentValidation;
using LintCoder.Application.Validation;
using Microsoft.Extensions.Localization;

namespace LintCoder.Application.Common.Security.Token
{
    public record TokenRequest(string Email, string Password);

    public class TokenRequestValidator : CustomValidator<TokenRequest>
    {
        public TokenRequestValidator(IStringLocalizer<TokenRequestValidator> T)
        {
            RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress()
                    .WithMessage(T["Invalid Email Address."]);

            RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
                .NotEmpty();
        }
    }
}
