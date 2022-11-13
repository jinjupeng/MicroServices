using LintCoder.RateLimiting.QoS;
using LintCoder.RateLimiting.QoS.Delegates;
using LintCoder.RateLimiting.QoS.Implement;
using LintCoder.RateLimiting.QoS.Implement.Handlers;
using LintCoder.RateLimiting.QoS.Options;
using LintCoder.RateLimiting.QoS.Rules;
using LintCoder.RateLimiting.QoS.Rules.Implement;
using LintCoder.RateLimiting.QoS.Services;
using LintCoder.RateLimiting.QoS.Services.Implement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class QoSServiceCollectionExtensions
    {
        public static IServiceCollection AddQoS(this IServiceCollection services, IConfiguration configuration, Action<QoSOptionsBuilder> configure = null)
        {
            #region Events

            var builder = new QoSOptionsBuilder();
            configure?.Invoke(builder);
            builder.InstallEvents();

            #endregion Events

            services.AddLogging();
            services.AddMemoryCache();
            services.AddOptions();
            services.Configure<QoSConfigureOptions>(configuration);

            services.AddTransient<IPollyFactory, PollyFactory>();
            services.AddTransient<IQoSProcessor, QoSProcessor>();
            services.AddTransient<IQuotaProcessor, QuotaProcessor>();
            services.TryAddEnumerable(ServiceDescriptor.Transient<IQuotaHandler, FixedWindowHandler>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IQuotaHandler, LeakyBucketQuotaHandler>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IQuotaHandler, SlidingWindowQuotaHandler>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IQuotaHandler, TokenBucketQuotaHandler>());

            services.AddTransient<IRuleMatcher, RuleMatcher>();
            services.AddSingleton<IRuleProvider, MemoryRuleProvider>();
            services.AddTransient<ICounterStoreService, MemoryCounterStoreService>();
            services.AddTransient<ILockerService, MonitorLockerService>();
            return services;
        }
    }
}