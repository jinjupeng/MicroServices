using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Identity.API.Application.Queries.Dict.QueryDict
{
    public class QueryDictCommandHandler : IRequestHandler<QueryDictCommand, MsgModel>
    {
        private readonly IdentityDbContext dbContext;

        public QueryDictCommandHandler(IdentityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<MsgModel> Handle(QueryDictCommand request, CancellationToken cancellationToken)
        {
            var sysDictList = await dbContext.SysDict.AsNoTracking().Where(x =>
                string.IsNullOrEmpty(request.GroupName) && x.GroupName.Contains(request.GroupName)
                || string.IsNullOrEmpty(request.GroupCode) && x.GroupCode.Contains(request.GroupCode)
            ).ToListAsync();
            return MsgModel.Success(sysDictList);
        }
    }
}
