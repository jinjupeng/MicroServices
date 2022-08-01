using LintCoder.Application.Common.Interfaces;
using LintCoder.Identity.API.Application.Models.DataTree;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.API.Application.Models.TreeNode;
using LintCoder.Identity.Domain.Entities;
using LintCoder.Identity.Infrastructure;
using LintCoder.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Identity.API.Application.Queries.Menu.QueryMenuTree
{
    public class QueryMenuTreeCommandHandler : IRequestHandler<QueryMenuTreeCommand, MsgModel>
    {
        private readonly IdentityDbContext dbContext;
        private readonly ICurrentUser userContext;

        public QueryMenuTreeCommandHandler(IdentityDbContext dbContext, ICurrentUser userContext) 
        {
            this.dbContext = dbContext;
            this.userContext = userContext;
        }


        public async Task<MsgModel> Handle(QueryMenuTreeCommand request, CancellationToken cancellationToken)
        {
            var sysMenuNodeQuery = from x in dbContext.SysMenu.AsNoTracking()
                         join y in dbContext.SysRoleMenu.AsNoTracking() on x.Id equals y.MenuId
                         join z in dbContext.SysUserRole.AsNoTracking() on y.RoleId equals z.RoleId
                         join m in dbContext.SysUser.AsNoTracking() on z.UserId equals m.Id
                         where m.Id == long.Parse(userContext.Id.ToString())
                         where x.Status == true // 启用
                         select new SysMenuNode
                         {
                             Id = x.Id,
                             MenuPid = x.MenuPid,
                             Name = x.MenuName,
                             Path = x.Url,
                             Icon = x.Icon
                         };
            var sysMenuNodeList = await sysMenuNodeQuery.ToListAsync();
            var result = DataTree<SysMenuNode, long>.BuildTreeWithoutRoot(sysMenuNodeList, default);
            return MsgModel.Success(result);
        }
    }
}
