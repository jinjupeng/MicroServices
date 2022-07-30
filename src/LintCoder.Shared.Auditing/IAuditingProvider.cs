

namespace LintCoder.Shared.Auditing
{
    public interface IAuditingProvider<T> where T : class
    {
        void AddAuditLog(T auditLog);

        Task AddAuditLogAsync(T auditLog);

    }
}
