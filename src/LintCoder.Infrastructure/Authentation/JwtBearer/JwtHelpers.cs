using LintCoder.Application.Common.Models;
using LintCoder.Infrastructure.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LintCoder.Infrastructure.Authentation.JwtBearer
{
    public static class JwtHelpers
    {
        /// <summary>
        /// create access token
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        public static string CreateAccessToken(TokenRequest tokenRequest, JwtSettings jwtSettings)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.Iat, jwtSettings.Iat.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, jwtSettings.Sub),
                new Claim(JwtRegisteredClaimNames.Aud, jwtSettings.Audience),
                new Claim(LintCoderClaims.UserName, tokenRequest.UserName),
                new Claim(LintCoderClaims.UserId, tokenRequest.UserId),
                new Claim(LintCoderClaims.FullName, tokenRequest.UserName),
                new Claim(LintCoderClaims.RoleCode, string.Join(",", tokenRequest.Roles)),
                new Claim(LintCoderClaims.TenantId, tokenRequest.TennantId),
                new Claim(LintCoderClaims.PhoneNumber, tokenRequest.PhoneNumber),
                new Claim(LintCoderClaims.EmailAddress, tokenRequest.Email),
                new Claim(LintCoderClaims.EmailVerified, tokenRequest.IsEmailConfirmed.ToString()),
                new Claim(LintCoderClaims.PhoneNumberVerified, tokenRequest.PhoneNumberVerified.ToString())
            };
            return WriteToken(claims, jwtSettings);
        }

        private static string WriteToken(List<Claim> claims, JwtSettings jwtSettings)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var expires = DateTime.Now.AddMinutes(jwtSettings.ExpireMinutes);
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Create a JwtBearerEvents instance
        /// </summary>
        /// <returns></returns>
        public static JwtBearerEvents GenarateJwtBearerEvents() =>
            new()
            {
                //接受到消息时调用
                OnMessageReceived = context => Task.CompletedTask
                    ,
                //在Token验证通过后调用
                OnTokenValidated = context =>
                {
                    return Task.CompletedTask;
                },
                //认证失败时调用
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))//如果是过期，在http heard中加入act参数
                        context.Response.Headers.Add("act", "expired");
                    return Task.CompletedTask;
                },
                //未授权时调用
                OnChallenge = context => Task.CompletedTask
            };

        /// <summary>
        /// create refres token
        /// </summary>
        /// <param name="oldToken"></param>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        public static string CreateRefreshToken(string oldToken, JwtSettings jwtSettings)
        {
            if (oldToken.StartsWith("Bearer "))
            {
                oldToken = oldToken.Substring(7);
            }
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(oldToken);
            var claims = jwtSecurityToken.Claims.ToList();
            claims.RemoveAll(x => x.Type == JwtRegisteredClaimNames.Jti);
            var claim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"));
            claims.Add(claim);

            return WriteToken(claims, jwtSettings);
        }

        public static TokenValidationParameters GenarateTokenValidationParameters(JwtSettings jwtSettings)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,// 是否验证Issuer
                ValidateAudience = true,// 是否验证Audience
                ValidateLifetime = true,// 是否验证失效时间
                ClockSkew = TimeSpan.FromSeconds(30),
                ValidateIssuerSigningKey = true,// 是否验证SecurityKey
                ValidAudience = jwtSettings.Audience,// Audience
                ValidIssuer = jwtSettings.Issuer,// Issuer，这两项和前面签发jwt的设置一致
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))// 拿到SecurityKey
            };

            return tokenValidationParameters;
        }

        public static IEnumerable<Claim> GetClaimsFromToken(string token, JwtSettings jwtSettings)
        {
            var parameters = GenarateTokenValidationParameters(jwtSettings);
            var tokenHandler = new JwtSecurityTokenHandler();
            var result = tokenHandler.ValidateToken(token, parameters, out var securityToken);
            if (result?.Identity?.IsAuthenticated != true)
                return Enumerable.Empty<Claim>();
            return result.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Jti);
        }
    }
}
