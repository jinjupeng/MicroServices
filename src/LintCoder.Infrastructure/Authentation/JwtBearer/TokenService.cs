using LintCoder.Application.Common.Security.Token;

namespace LintCoder.Infrastructure.Authentation.JwtBearer
{
    internal class TokenService : ITokenService
    {
        public Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
