using LintCoder.Identity.Domain.Entities;
using LintCoder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Identity.API.Infrastructure.Services
{
    public class SysApiService : ISysApiService
    {
        private readonly IBaseRepository<SysApi> _baseService;
        private readonly IBaseRepository<SysRoleApi> _baseSysRoleApiService;
        private readonly IBaseRepository<SysRole> _baseSysRoleService;
        private readonly IBaseRepository<SysUserRole> _baseSysUserRoleService;

        public SysApiService(IBaseRepository<SysApi> baseService, 
            IBaseRepository<SysRoleApi> baseSysRoleApiService, 
            IBaseRepository<SysRole> baseSysRoleService,
            IBaseRepository<SysUserRole> baseSysUserRoleService)
        {
            _baseService = baseService;
            _baseSysRoleApiService = baseSysRoleApiService;
            _baseSysRoleService = baseSysRoleService;
            _baseSysUserRoleService = baseSysUserRoleService;
        }

        public async Task<List<string>> GetAllApiOfRoleAsync(List<long> roleIds)
        {
            var sysRoleList = await _baseSysRoleService.GetModels(x => x.Status == false && roleIds.Contains(x.Id)).ToListAsync(); // 获取未禁用的角色
            var roleIdList = sysRoleList.Select(x => x.Id).Distinct();
            var sysRoleApiList = await _baseSysRoleApiService.GetModels(x => roleIdList.Contains(x.RoleId)).ToListAsync();
            var roleApiIds = sysRoleApiList.Select(x => x.Id).Distinct();
            var sysApiList = await _baseService.GetModels(x => x.Status == false && roleApiIds.Contains(x.Id)).ToListAsync();  // 获取未禁用的接口

            return sysApiList.Select(x => x.Url).ToList();
        }

        public async Task<List<string>> GetAllApiOfUserAsync(List<long> userIds)
        {
            var sysUserRoleList = await _baseSysUserRoleService.GetModels(x => userIds.Contains(x.UserId)).ToListAsync();
            var roleIds = sysUserRoleList.Select(x => x.Id).Distinct().ToList();
            return await GetAllApiOfRoleAsync(roleIds);
        }
    }
}
