using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.Dept.DeleteDept
{
    public class DeleteDeptCommand : IRequest<MsgModel>
    {
        public string Id { get; set; }
    }
}
