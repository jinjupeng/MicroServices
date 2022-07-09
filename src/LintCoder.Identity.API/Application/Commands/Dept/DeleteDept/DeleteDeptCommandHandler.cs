using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.Dept.DeleteDept
{
    public class DeleteDeptCommandHandler : IRequestHandler<DeleteDeptCommand, MsgModel>
    {
        public Task<MsgModel> Handle(DeleteDeptCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
