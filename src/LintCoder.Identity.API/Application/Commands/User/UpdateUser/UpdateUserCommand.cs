using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.User.UpdateUser
{
    public class UpdateUserCommand : IRequest<MsgModel>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string NickName { get; set; }

        public string Portrait { get; set; }

        public long OrgId { get; set; }

        public bool Enabled { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int Sex { get; set; }
    }
}
