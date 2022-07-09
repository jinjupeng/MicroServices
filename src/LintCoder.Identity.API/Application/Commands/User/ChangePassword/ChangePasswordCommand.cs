using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.User.ChangePassword
{
    public class ChangePasswordCommand : IRequest<MsgModel>
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
