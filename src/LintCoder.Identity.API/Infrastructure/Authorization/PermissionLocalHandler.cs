using LintCoder.Identity.API.Infrastructure.Services;
using LintCoder.Shared.Authorization;

namespace LintCoder.Identity.API.Infrastructure.Authorization
{
    public sealed class PermissionLocalHandler : AbstractPermissionHandler
    {
        private readonly ISysApiService _sysApiService;

        public PermissionLocalHandler(ISysApiService sysApiService)
        {
            _sysApiService = sysApiService;
        }

        protected override async Task<bool> CheckUserPermissions(long userId, IEnumerable<string> requestPermissions, IEnumerable<string> userBelongsRoleIds)
        {
            var roleIds = userBelongsRoleIds.Select(Int64.Parse).ToList();
            // 当前登录用户所属角色的权限
            var userPermissions = await _sysApiService.GetAllApiOfRoleAsync(roleIds);
            if (userPermissions == null || !userPermissions.Any() || requestPermissions == null || !requestPermissions.Any())
            {
                return false;
            }
            // 两个集合的交集
            if (userPermissions.Intersect(requestPermissions).Any())
            {
                // 根据用户和用户所属的角色，获取权限，再根据传入的权限来判断是否有权限
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
