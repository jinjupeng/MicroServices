using LintCoder.Shared.Auditing.WriteToElastic;
using LintCoder.Shared.Auditing.WriteToMonogDB;
using LintCoder.Shared.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LintCoder.Shared.Auditing
{
    public static class AuditLogExtensions
    {
        public static IAuditLogBuilder AddAuditLog(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IAuditLogBuilder, AuditLogBuilder>();

            IAuditLogBuilder builder = services.BuildServiceProvider().GetService<IAuditLogBuilder>();

            return builder;
        }

        public static IAuditLogBuilder WriteToElastic(this IAuditLogBuilder auditLogBuilder, Action<AuditLogElasticOptions> elasticOptions)
        {
            if (elasticOptions == null)
            {
                throw new ArgumentNullException(nameof(elasticOptions));
            }
            auditLogBuilder.Services.Configure(elasticOptions);

            auditLogBuilder.Services.AddScoped<IAuditingProvider<AuditLogInfo>, AuditLogElasticHandler<AuditLogInfo>>();

            return auditLogBuilder;
        }

        public static IAuditLogBuilder WriteToMongoDB(this IAuditLogBuilder auditLogBuilder, Action<MongoOptions> mongoOptions)
        {
            auditLogBuilder.Services.AddMongoOptions(mongoOptions);

            auditLogBuilder.Services.AddScoped<IAuditingProvider<AuditLogMongoDBInfo>, AuditLogMongoDBHandler>();

            return auditLogBuilder;
        }

        public static IAuditLogBuilder WriteToMongoDB(this IAuditLogBuilder auditLogBuilder, IConfiguration configuration)
        {
            auditLogBuilder.Services.AddMongoOptions(configuration);
            auditLogBuilder.Services.AddScoped<IAuditingProvider<AuditLogMongoDBInfo>, AuditLogMongoDBHandler>();

            return auditLogBuilder;
        }
    }
}
