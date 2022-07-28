namespace LintCoder.Shared.Auditing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class IgnoreAuditingAttribute : Attribute
    {

    }
}