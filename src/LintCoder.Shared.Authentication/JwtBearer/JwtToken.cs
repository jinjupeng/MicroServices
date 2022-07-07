using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LintCoder.Shared.Authentication.JwtBearer
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
