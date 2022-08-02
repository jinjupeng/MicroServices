

namespace LintCoder.Application.Common.Security.Token
{
    public record TokenResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
}
