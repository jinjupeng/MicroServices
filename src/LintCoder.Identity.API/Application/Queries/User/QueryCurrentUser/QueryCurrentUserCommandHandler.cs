using LintCoder.Application.Common.Interfaces;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Infrastructure;
using LintCoder.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Identity.API.Application.Queries.User.QueryCurrentUser
{
    public class QueryCurrentUserCommandHandler : IRequestHandler<QueryCurrentUserCommand, MsgModel>
    {
        private readonly IdentityDbContext dbContext;
        private readonly ICurrentUser userContext;

        public QueryCurrentUserCommandHandler(IdentityDbContext dbContext, ICurrentUser userContext)
        {
            this.dbContext = dbContext;
            this.userContext = userContext;
        }

        public async Task<MsgModel> Handle(QueryCurrentUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await dbContext.SysUser.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userContext.UserId);
            if(currentUser == null)
            {
                return MsgModel.Fail("用户不存在！");
            }
            currentUser.Password = "";
            return MsgModel.Success(currentUser);
        }
    }
}
