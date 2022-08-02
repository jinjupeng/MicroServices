using Microsoft.AspNetCore.Authorization;

namespace LintCoder.Infrastructure.Authorization
{
    public class LCRoleAuthorizeAttribute : AuthorizeAttribute
    {
        public LCRoleAuthorizeAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
