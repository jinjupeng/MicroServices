namespace LintCoder.Shared.Security.Security;

public interface IHashService
{
    string Create(string value, string salt);
}
