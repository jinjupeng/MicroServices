using LintCoder.Application.Common.Interfaces;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Identity.API.Application.Queries.Config.QueryConfig
{
    public class QueryConfigCommandHandler : IRequestHandler<QueryConfigCommand, MsgModel>
    {
        private readonly IdentityDbContext dbContext;
        private readonly ICurrentUser userContext;

        public QueryConfigCommandHandler(IdentityDbContext dbContext, ICurrentUser userContext)
        {
            this.dbContext = dbContext;
            this.userContext = userContext;
        }
        public async Task<MsgModel> Handle(QueryConfigCommand request, CancellationToken cancellationToken)
        {
            var configList = await dbContext.SysConfig.AsNoTracking().Where(x =>
                string.IsNullOrWhiteSpace(request.Keyword)
                || x.ParamKey.Contains(request.Keyword)
                || x.ParamValue.Contains(request.Keyword)
            ).ToListAsync();
            return MsgModel.Success(configList);
        }
    }
}
