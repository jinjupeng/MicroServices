using LintCoder.Application.Common.Interfaces;
using System.Security.Claims;

namespace LintCoder.Infrastructure.Users 
{ 

    public class CurrentUser : ICurrentUser, ICurrentUserInitializer
    {
        private ClaimsPrincipal? _user;

        public string? Name => _user?.Identity?.Name;

        public Guid? Id => throw new NotImplementedException();

        public string UserName => throw new NotImplementedException();

        public string PhoneNumber => throw new NotImplementedException();

        public bool PhoneNumberVerified => throw new NotImplementedException();

        public string Email => throw new NotImplementedException();

        public bool EmailVerified => throw new NotImplementedException();

        public Guid? TenantId => throw new NotImplementedException();

        public string[] Roles => throw new NotImplementedException();

        Guid ICurrentUser.Id => throw new NotImplementedException();

        private Guid _userId = Guid.Empty;

        public Guid GetUserId() =>
            IsAuthenticated()
                ? Guid.Parse(_user?.GetUserId() ?? Guid.Empty.ToString())
                : _userId;

        public string? GetUserEmail() =>
            IsAuthenticated()
                ? _user!.GetEmail()
                : string.Empty;

        public bool IsAuthenticated() =>
            _user?.Identity?.IsAuthenticated is true;

        public bool IsInRole(string role) =>
            _user?.IsInRole(role) is true;

        public IEnumerable<Claim>? GetUserClaims() =>
            _user?.Claims;

        public string? GetTenant() =>
            IsAuthenticated() ? _user?.GetTenant() : string.Empty;

        public void SetCurrentUser(ClaimsPrincipal user)
        {
            if (_user != null)
            {
                throw new Exception("Method reserved for in-scope initialization");
            }

            _user = user;
        }

        public void SetCurrentUserId(string userId)
        {
            if (_userId != Guid.Empty)
            {
                throw new Exception("Method reserved for in-scope initialization");
            }

            if (!string.IsNullOrEmpty(userId))
            {
                _userId = Guid.Parse(userId);
            }
        }

        public Claim FindClaim(string claimType)
        {
            throw new NotImplementedException();
        }

        public Claim[] FindClaims(string claimType)
        {
            throw new NotImplementedException();
        }

        public Claim[] GetAllClaims()
        {
            throw new NotImplementedException();
        }
    }
}
