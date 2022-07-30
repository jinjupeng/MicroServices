using Microsoft.Extensions.DependencyInjection;

namespace LintCoder.Shared.Auditing
{
    /// <summary>
    /// 添加审计日志构造器
    /// </summary>
    public interface IAuditLogBuilder
    {
        /// <summary>
        /// 获取 服务集合
        /// </summary>
        IServiceCollection Services { get; }
    }
}
