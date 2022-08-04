using LintCoder.Identity.API.Application.Models.Request;
using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.User.UpdateUser
{
    public class UpdateUserCommand : IRequest<MsgModel>
    {
        public string Id { get; set; }

        public UpdateUserRequest UpdateUserRequest { get; set; }
    }

}
