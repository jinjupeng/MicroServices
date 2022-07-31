using LintCoder.Application.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LintCoder.Shared.Authorization
{
    public abstract class AbstractPermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated && context.Resource is HttpContext httpContext)
            {
                var authHeader = httpContext.Request.Headers["Authorization"].ToString();
                if (authHeader != null && authHeader.StartsWith("Bearer"))
                { 
                    context.Succeed(requirement);
                    return;
                }

                var userContext = httpContext.RequestServices.GetService<UserContext>();
                // 请求Url
                var requestPermission = httpContext.Request.Path.Value.ToLower().Replace("/api", "");
                var result = await CheckUserPermissions(userContext.Id, new List<string> { requestPermission }, userContext.RoleIds);
                if (result)
                {
                    context.Succeed(requirement);
                    return;
                }
            }
            context.Fail();
        }

        protected abstract Task<bool> CheckUserPermissions(long userId, IEnumerable<string> requestPermissions, string userBelongsRoleIds);
    }
}