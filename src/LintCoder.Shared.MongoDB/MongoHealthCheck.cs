using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LintCoder.Shared.MongoDB
{
    public class MongoHealthCheck : IHealthCheck
    {
        private readonly MongoContext _context;

        public MongoHealthCheck(MongoContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthCheckResultHealthy = await CheckMongoDBConnectionAsync();


            if (healthCheckResultHealthy)
            {
                return HealthCheckResult.Healthy("MongoDB health check success");
            }

            return HealthCheckResult.Unhealthy("MongoDB health check failure"); ;
        }

        private async Task<bool> CheckMongoDBConnectionAsync()
        {
            try
            {
                await _context.Database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
            }

            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
