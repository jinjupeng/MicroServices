namespace LintCoder.Shared.Security.Security;

public interface ICryptographyService
{
    string Decrypt(string value, string salt);

    string Encrypt(string value, string salt);
}
