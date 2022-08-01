using System.Security.Claims;

namespace LintCoder.Infrastructure.Users
{
    public static class LintCoderClaims
    {
        /// <summary>
        /// Default: <see cref="ClaimTypes.Name"/>
        /// </summary>
        public static string UserName { get; set; } = ClaimTypes.Name;

        /// <summary>
        /// Default: <see cref="ClaimTypes.NameIdentifier"/>
        /// </summary>
        public static string UserId { get; set; } = ClaimTypes.NameIdentifier;

        /// <summary>
        /// Default: <see cref="ClaimTypes.Role"/>
        /// </summary>
        public static string Role { get; set; } = ClaimTypes.Role;

        /// <summary>
        /// Default: <see cref="ClaimTypes.Email"/>
        /// </summary>
        public static string Email { get; set; } = ClaimTypes.Email;

        /// <summary>
        /// Default: "full_name".
        /// </summary>
        public static string FullName { get; set; } = "full_name";

        /// <summary>
        /// Default: "image_url".
        /// </summary>
        public static string ImageUrl { get; set; } = "image_url";

        /// <summary>
        /// Default: "expiration".
        /// </summary>
        public static string Expiration { get; set; } = "expiration";

        /// <summary>
        /// Default: "email_verified".
        /// </summary>
        public static string EmailVerified { get; set; } = "email_verified";

        /// <summary>
        /// Default: "phone_number".
        /// </summary>
        public static string PhoneNumber { get; set; } = "phone_number";

        /// <summary>
        /// Default: "phone_number_verified".
        /// </summary>
        public static string PhoneNumberVerified { get; set; } = "phone_number_verified";

        /// <summary>
        /// Default: "tenantid".
        /// </summary>
        public static string TenantId { get; set; } = "tenantid";


        /// <summary>
        /// Default: "editionid".
        /// </summary>
        public static string EditionId { get; set; } = "editionid";

        /// <summary>
        /// Default: "client_id".
        /// </summary>
        public static string ClientId { get; set; } = "client_id";
    }
}
