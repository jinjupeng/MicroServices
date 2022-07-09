using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LintCoder.Shared.Authentication.JwtBearer
{
    /// <summary>
    /// jwt帮助类
    /// </summary>
    public class JwtHelper
    {
        private readonly IOptions<JwtOptions> _jwtOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtOptions"></param>
        public JwtHelper(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        /// <summary>
        /// create access token
        /// </summary>
        /// <param name="uniqueName"></param>
        /// <param name="nameId"></param>
        /// <param name="name"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string CreateAccessToken(
            string uniqueName
            , string nameId
            , string name
            , string roleIds
            )
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtClaimNames.UserName, uniqueName),
                new Claim(JwtClaimNames.UserId, nameId),
                new Claim(JwtClaimNames.NickName, name),
                new Claim(JwtClaimNames.RoleIds, roleIds)
            };
            return WriteToken(claims);
        }

        private string WriteToken(List<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, _jwtOptions.Value.Iat.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, _jwtOptions.Value.Sub));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey));

            string issuer = _jwtOptions.Value.Issuer;
            string audience = _jwtOptions.Value.Audience;
            var expires = DateTime.Now.AddMinutes(_jwtOptions.Value.ExpireMinutes);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// create refres token
        /// </summary>
        /// <returns></returns>
        /// <param name="oldToken"></param>
        public string CreateRefreshToken(string oldToken)
        {
            if(oldToken.StartsWith("Bearer "))
            {
                oldToken = oldToken.Substring(7);
            }
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(oldToken);
            var claims = jwtSecurityToken.Claims.ToList();
            claims.RemoveAll(x => x.Type == JwtRegisteredClaimNames.Jti 
                || x.Type == JwtRegisteredClaimNames.Sub 
                || x.Type == JwtRegisteredClaimNames.Iat
                || x.Type == JwtRegisteredClaimNames.Aud);
            var claim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"));
            claims.Add(claim);

            return WriteToken(claims);
        }

        public TokenValidationParameters GenarateTokenValidationParameters()
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,// 是否验证Issuer
                ValidateAudience = true,// 是否验证Audience
                ValidateLifetime = true,// 是否验证失效时间
                ClockSkew = TimeSpan.FromSeconds(30),
                ValidateIssuerSigningKey = true,// 是否验证SecurityKey
                ValidAudience = _jwtOptions.Value.Audience,// Audience
                ValidIssuer = _jwtOptions.Value.Issuer,// Issuer，这两项和前面签发jwt的设置一致
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey))// 拿到SecurityKey
            };

            return tokenValidationParameters;
        }

        /// <summary>
        /// get claim from refesh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="claimName"></param>
        /// <returns></returns>
        public Claim GetClaimFromRefeshToken(string refreshToken, string claimName = JwtClaimNames.UserId)
        {
            var parameters = GenarateTokenValidationParameters();
            var tokenHandler = new JwtSecurityTokenHandler();
            var result = tokenHandler.ValidateToken(refreshToken, parameters, out var securityToken);
            if (!result.Identity.IsAuthenticated)
                return null;
            return result.Claims.FirstOrDefault(x => x.Type == claimName);
        }

    }
}
