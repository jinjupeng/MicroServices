

namespace LintCoder.Infrastructure.Auth.JwtBearer
{
    public record JwtToken
    {
        public JwtToken(string token, DateTime expire)
        {
            Token = token;
            Expire = expire;
        }
        public string Token { get; set; }
        public DateTime Expire { get; set; }
    }
}
