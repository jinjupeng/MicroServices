using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Domain.Entities;
using LintCoder.Infrastructure.Persistence;
using MediatR;

namespace LintCoder.Identity.API.Application.Queries.User.GetUsers
{
    public class QueryUsersCommandHandler : IRequestHandler<QueryUsersCommand, MsgModel>
    {
        private readonly IBaseRepository<SysUser> _baseRepository;

        public QueryUsersCommandHandler(IBaseRepository<SysUser> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<MsgModel> Handle(QueryUsersCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(MsgModel.Success(request));
        }
    }
}
