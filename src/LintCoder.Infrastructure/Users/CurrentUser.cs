using LintCoder.Application.Common.Interfaces;
using System.Security.Claims;

namespace LintCoder.Infrastructure.Users
{
    public class CurrentUser : ICurrentUser, ICurrentUserInitializer
    {
        private ClaimsPrincipal? _user;

        public string UserId => _user?.GetUserId();

        public string UserName => _user?.GetFullName();

        public string PhoneNumber => _user?.GetPhoneNumber();

        public bool PhoneNumberVerified => (bool)(_user?.GetPhoneNumberVerified());

        public string Email => _user?.GetEmail();

        public bool EmailVerified => (bool)(_user?.GetEmailVerified());

        public string TenantId => _user?.GetTenant();

        public string[] Roles => _user?.GetRoles();

        public Claim FindClaim(string claimType)
        {
            return _user.Claims.FirstOrDefault(x => x.Type == claimType) ?? null;
        }

        public Claim[] FindClaims(string claimType)
        {
            return _user.Claims.Where(x => x.Type == claimType).ToArray();
        }

        public Claim[] GetAllClaims()
        {
            return _user.Claims.ToArray();
        }

        public void SetCurrentUser(ClaimsPrincipal user)
        {
            if (_user != null)
            {
                throw new Exception("Method reserved for in-scope initialization");
            }

            _user = user;
        }
    }
}
