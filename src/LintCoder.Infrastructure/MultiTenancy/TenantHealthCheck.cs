using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LintCoder.Infrastructure.MultiTenancy
{
    public class TenantHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
