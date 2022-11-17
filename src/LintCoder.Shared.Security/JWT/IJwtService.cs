using System.Security.Claims;

namespace LintCoder.Shared.Security.Security;

public interface IJwtService
{
    Dictionary<string, object> Decode(string token);

    string Encode(IList<Claim> claims);
}
