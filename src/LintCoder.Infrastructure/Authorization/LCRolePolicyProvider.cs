using Microsoft.AspNetCore.Authorization;

namespace LintCoder.Infrastructure.Authorization
{
    internal class LCRolePolicyProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            throw new NotImplementedException();
        }
    }
}
