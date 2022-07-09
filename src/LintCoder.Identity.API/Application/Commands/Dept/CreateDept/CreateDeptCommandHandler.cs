using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.Dept.CreateDept
{
    public class CreateDeptCommandHandler : IRequestHandler<CreateDeptCommand, MsgModel>
    {
        public Task<MsgModel> Handle(CreateDeptCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
