using System.Security.Claims;

namespace LintCoder.Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        Guid GetUserId();

        string? GetUserEmail();

        string? GetTenant();

        bool IsAuthenticated();

        bool IsInRole(string role);

        IEnumerable<Claim>? GetUserClaims();

        Guid Id { get; }

        string UserName { get; }

        string PhoneNumber { get; }

        bool PhoneNumberVerified { get; }

        string Email { get; }

        bool EmailVerified { get; }

        Guid? TenantId { get; }

        string[] Roles { get; }

        Claim FindClaim(string claimType);

        Claim[] FindClaims(string claimType);

        Claim[] GetAllClaims();
    }
}
