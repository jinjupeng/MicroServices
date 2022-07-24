using Microsoft.Extensions.DependencyInjection;

namespace LintCoder.EventBus.Dapr
{
    public static class DaprHealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddDapr(this IHealthChecksBuilder builder) =>
            builder.AddCheck<DaprHealthCheck>("dapr");
    }
}
