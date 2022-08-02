using Microsoft.AspNetCore.Authorization;

namespace LintCoder.Infrastructure.Authorization
{
    internal class LCRoleRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> AllowRoles { get; }
        public LCRoleRequirement(params string[] allowRoles)
        {
            AllowRoles = allowRoles;
        }
    }
}
