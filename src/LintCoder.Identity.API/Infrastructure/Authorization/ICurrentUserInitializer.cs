using System.Security.Claims;

namespace LintCoder.Identity.API.Infrastructure.Authorization
{
    public interface ICurrentUserInitializer
    {
        void SetCurrentUser(ClaimsPrincipal user);

        void SetCurrentUserId(string userId);
    }
}
