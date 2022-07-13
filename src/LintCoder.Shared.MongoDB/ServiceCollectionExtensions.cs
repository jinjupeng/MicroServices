using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LintCoder.Shared.MongoDB
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoOptions(this IServiceCollection services, Action<MongoOptions> mongoOptions)
        {
            if (mongoOptions == null)
            {
                throw new ArgumentNullException(nameof(mongoOptions));
            }
            services.Configure(mongoOptions);
            services.AddSingleton<MongoContext>();

            return services;
        }

        public static IServiceCollection AddMongoOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection(nameof(MongoOptions)));
            services.AddSingleton<MongoContext>();

            return services;
        }

        public static IServiceCollection AddMongoHealthCheck(this IServiceCollection services)
        {
            IHealthChecksBuilder healthChecks = services.AddHealthChecks();
            healthChecks.AddCheck<MongoHealthCheck>("MongoHealthCheck");

            return services;
        }
    }
}
