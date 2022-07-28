using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace LintCoder.Shared.Auditing
{
    public static class AuditingExtensions
    {

        public static IServiceCollection AddAuditing(this IServiceCollection services)
        {
            services.AddSingleton(new ProxyGenerator());
            services.AddScoped<IInterceptor, AuditingInterceptor>();

            return services;
        }
    }
}
