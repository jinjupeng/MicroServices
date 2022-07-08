using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.User.DeleteUser
{
    public class DeleteUserCommand : IRequest<MsgModel>
    {
        public long Id { get; set; }
    }
}
