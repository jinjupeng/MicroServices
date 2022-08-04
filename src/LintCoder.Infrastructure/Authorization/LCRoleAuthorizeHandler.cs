using LintCoder.Infrastructure.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace LintCoder.Infrastructure.Authorization
{
    internal class LCRoleAuthorizeHandler : AuthorizationHandler<LCRoleRequirement>
    {
        // https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/security/authorization/policies.md
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LCRoleRequirement requirement)
        {
            LCRoleAuthorizeAttribute? lcRoleAttribute = null;
            if (context.Resource is HttpContext httpContext)
            {
                lcRoleAttribute = httpContext.GetEndpoint()?.Metadata.GetMetadata<LCRoleAuthorizeAttribute>();
            }
            if (context.Resource is Endpoint endpoint)
            {
                lcRoleAttribute = endpoint?.Metadata.GetMetadata<LCRoleAuthorizeAttribute>();
            }
            if(context.Resource is AuthorizationFilterContext mvcContext)
            {
                lcRoleAttribute = mvcContext.HttpContext.GetEndpoint()?.Metadata.GetMetadata<LCRoleAuthorizeAttribute>();
            }

            if (!context.User.HasClaim(x => x.Type == LintCoderClaims.RoleCode))
                return Task.CompletedTask;

            var claim = context.User
                .Claims.FirstOrDefault(x => x.Type == LintCoderClaims.RoleCode);
            var role = claim?.Value ?? "";

            var allowRoles = requirement.AllowRoles;
            var lcRole = lcRoleAttribute?.Roles ?? "";

            if (allowRoles.Contains(role) && lcRole.Contains(role))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
