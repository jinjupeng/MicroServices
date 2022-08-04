namespace LintCoder.Identity.API.Infrastructure.Services
{
    public interface ISysApiService
    {
        /// <summary>
        /// 根据角色ID获取角色权限
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<List<string>> GetAllApiOfRoleAsync(List<string> roleIds);

        /// <summary>
        /// 根据用户ID获取用户所属角色权限
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task<List<string>> GetAllApiOfUserAsync(List<string> userIds);
    }
}
