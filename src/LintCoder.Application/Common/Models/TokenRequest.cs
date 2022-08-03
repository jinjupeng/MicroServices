

namespace LintCoder.Application.Common.Models
{
    public class TokenRequest
    {
        public string UserName { get; set; }

        public string UserId { get; set; }

        public string TennantId { get; set; }

        public string ImageUrl { get;set; }

        public IEnumerable<string> Roles { get;set;} 

        public string Email { get;set; }

        public bool IsEmailConfirmed { get; set; } = false;

        public string PhoneNumber { get;set; }

        public bool PhoneNumberVerified { get;set; } = false;
    }
}
