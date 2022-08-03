using System.Security.Claims;

namespace LintCoder.Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        string UserId { get; }

        string UserName { get; }

        string PhoneNumber { get; }

        bool PhoneNumberVerified { get; }

        string Email { get; }

        bool EmailVerified { get; }

        string TenantId { get; }

        string[] Roles { get; }

        Claim FindClaim(string claimType);

        Claim[] FindClaims(string claimType);

        Claim[] GetAllClaims();
    }
}
