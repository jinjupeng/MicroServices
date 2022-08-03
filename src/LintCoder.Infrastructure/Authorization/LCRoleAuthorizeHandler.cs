using LintCoder.Infrastructure.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace LintCoder.Infrastructure.Authorization
{
    internal class LCRoleAuthorizeHandler : AuthorizationHandler<LCRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LCRoleRequirement requirement)
        {
            var endpoint = context.Resource as RouteEndpoint;
            var lcRoleAttribute = endpoint?.Metadata.GetMetadata<LCRoleAuthorizeAttribute>();

            if (!context.User.HasClaim(x => x.Type == LintCoderClaims.Role))
                return Task.CompletedTask;

            var claim = context.User
                .Claims.FirstOrDefault(x => x.Type == LintCoderClaims.Role);
            var role = claim?.Value ?? "";

            var allowRoles = requirement.AllowRoles;
            var lcRole = lcRoleAttribute?.Roles ?? "";

            if (allowRoles.Contains(role) && lcRole.Contains(role))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
