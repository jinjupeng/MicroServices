using Lintcoder.Base;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Domain.Entities;
using MediatR;

namespace LintCoder.Identity.API.Application.Queries.User.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, MsgModel>
    {
        private readonly IBaseRepository<SysUser> _baseRepository;

        public GetUsersQueryHandler(IBaseRepository<SysUser> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<MsgModel> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(MsgModel.Success(request));
        }
    }
}
