using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.Account.Login
{
    public class LoginCommand : IRequest<MsgModel>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
