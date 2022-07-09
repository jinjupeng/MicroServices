using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.Dept.UpdateDept
{
    public class UpdateDeptCommandHandler : IRequestHandler<UpdateDeptCommand, MsgModel>
    {
        public Task<MsgModel> Handle(UpdateDeptCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
