using Common.Utility.Utils;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Infrastructure;
using LintCoder.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Identity.API.Application.Queries.User.QueryPasswordIsDefault
{
    public class QueryPasswordIsDefaultCommandHandler : IRequestHandler<QueryPasswordIsDefaultCommand, MsgModel>
    {
        private readonly IdentityDbContext dbContext;
        private readonly UserContext userContext;

        public QueryPasswordIsDefaultCommandHandler(IdentityDbContext dbContext, UserContext userContext)
        {
            this.dbContext = dbContext;
            this.userContext = userContext;
        }

        public async Task<MsgModel> Handle(QueryPasswordIsDefaultCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await dbContext.SysUser.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userContext.Id);
            if (currentUser == null)
            {
                return MsgModel.Fail("用户不存在！");
            }
            var sysConfig = await dbContext.SysConfig.AsNoTracking().FirstOrDefaultAsync(x => x.ParamKey == "user.init.password");
            var initPassword = PasswordEncoder.Encode(sysConfig == null ? "123456" : sysConfig.ParamValue);
            var result = currentUser.Password == initPassword;
            return MsgModel.Success(result);
        }
    }
}
