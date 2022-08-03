

namespace LintCoder.Infrastructure.Users
{
    public static class LintCoderClaims
    {
        /// <summary>
        /// </summary>
        public static string UserName { get; set; } ="user_name";

        /// <summary>
        /// </summary>
        public static string UserId { get; set; } = "user_id";

        /// <summary>
        /// </summary>
        public static string Role { get; set; } = "role";

        /// <summary>
        /// </summary>
        public static string Email { get; set; } = "email";

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
        public static string TenantId { get; set; } = "tenant_id";

        /// <summary>
        /// Default: "client_id".
        /// </summary>
        public static string ClientId { get; set; } = "client_id";
    }
}
