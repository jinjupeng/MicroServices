using Microsoft.Extensions.DependencyInjection;

namespace LintCoder.Shared.Auditing
{
    public class AuditLogBuilder
    {
        /// <summary>
        /// 获取 服务集合
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 初始化一个<see cref="AuditLogBuilder"/>类型的新实例
        /// </summary>
        public AuditLogBuilder(IServiceCollection services)
        {
            Services = services;
        }
    }
}
