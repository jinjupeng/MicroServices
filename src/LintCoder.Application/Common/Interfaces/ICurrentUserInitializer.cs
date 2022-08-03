using System.Security.Claims;

namespace LintCoder.Application.Common.Interfaces
{
    public interface ICurrentUserInitializer
    {
        void SetCurrentUser(ClaimsPrincipal user);
    }
}
