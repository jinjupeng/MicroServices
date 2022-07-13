using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LintCoder.Identity.API.HealthChecks
{
    public class RedisHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = await RedisHelper.PingAsync();

            if (isHealthy)
            {
                return HealthCheckResult.Healthy("A healthy result.");
            }
            else
            {
                return
                    new HealthCheckResult(
                        context.Registration.FailureStatus, "An unhealthy result.");
            }

        }
    }
}
