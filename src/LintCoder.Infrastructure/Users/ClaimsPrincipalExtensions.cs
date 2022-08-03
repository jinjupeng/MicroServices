using System.Security.Claims;

namespace LintCoder.Infrastructure.Users
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool GetPhoneNumberVerified(this ClaimsPrincipal principal)
        {
            var claimValue = principal.FindFirstValue(LintCoderClaims.PhoneNumberVerified);
            if (string.IsNullOrEmpty(claimValue))
            {
                return false;
            }
            return bool.Parse(claimValue);
        }

        public static bool GetEmailVerified(this ClaimsPrincipal principal)
        {
            var claimValue = principal.FindFirstValue(LintCoderClaims.EmailVerified);
            if (string.IsNullOrEmpty(claimValue))
            {
                return false;
            }
            return bool.Parse(claimValue);
        }

        public static string[] GetRoles(this ClaimsPrincipal principal)
        {
            var claimValue = principal.FindFirstValue(LintCoderClaims.RoleCode);
            if (string.IsNullOrEmpty(claimValue))
            {
                return Array.Empty<string>();
            }
            return claimValue.Split(",");
        }

        public static string? GetEmail(this ClaimsPrincipal principal)
            => principal.FindFirstValue(ClaimTypes.Email);

        public static string? GetTenant(this ClaimsPrincipal principal)
            => principal.FindFirstValue(LintCoderClaims.TenantId);

        public static string? GetFullName(this ClaimsPrincipal principal)
            => principal?.FindFirst(LintCoderClaims.FullName)?.Value;

        public static string? GetFirstName(this ClaimsPrincipal principal)
            => principal?.FindFirst(ClaimTypes.Name)?.Value;

        public static string? GetSurname(this ClaimsPrincipal principal)
            => principal?.FindFirst(ClaimTypes.Surname)?.Value;

        public static string? GetPhoneNumber(this ClaimsPrincipal principal)
            => principal.FindFirstValue(ClaimTypes.MobilePhone);

        public static string? GetUserId(this ClaimsPrincipal principal)
           => principal.FindFirstValue(ClaimTypes.NameIdentifier);

        public static string? GetImageUrl(this ClaimsPrincipal principal)
           => principal.FindFirstValue(LintCoderClaims.ImageUrl);

        public static DateTimeOffset GetExpiration(this ClaimsPrincipal principal) =>
            DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(
                principal.FindFirstValue(LintCoderClaims.Expiration)));

        private static string? FindFirstValue(this ClaimsPrincipal principal, string claimType) =>
            principal is null
                ? throw new ArgumentNullException(nameof(principal))
                : principal.FindFirst(claimType)?.Value;
    }
}