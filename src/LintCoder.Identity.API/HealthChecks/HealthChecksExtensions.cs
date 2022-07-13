using HealthChecks.UI.Client;
using LintCoder.Identity.Infrastructure;
using LintCoder.Shared.MongoDB;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text;
using System.Text.Json;

namespace LintCoder.Identity.API.HealthChecks
{
    /// <summary>
    /// HealthChecks Extensions.
    /// </summary>
    public static class HealthChecksExtensions
    {
        /// <summary>
        /// Add Health Checks dependencies varying on configuration.
        /// </summary>
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            IHealthChecksBuilder healthChecks = services.AddHealthChecks();

            healthChecks.AddDbContextCheck<IdentityDbContext>("IdentityDbContext");
            healthChecks.AddCheck<RedisHealthCheck>("RedisHealthCheck");
            healthChecks.AddCheck("self", () => HealthCheckResult.Healthy());

            services.AddMongoHealthCheck();

            return services;
        }

        /// <summary>
        ///     Use Health Checks dependencies.
        /// </summary>
        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health",
                new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    // ResponseWriter = WriteResponse
                });
            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });

            return app;
        }

        /// <summary>
        /// 自定义响应
        /// </summary>
        /// <param name="context"></param>
        /// <param name="healthReport"></param>
        /// <returns></returns>
        private static Task WriteResponse(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions { Indented = true };

            using var memoryStream = new MemoryStream();
            using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("status", healthReport.Status.ToString());
                jsonWriter.WriteStartObject("results");

                foreach (var healthReportEntry in healthReport.Entries)
                {
                    jsonWriter.WriteStartObject(healthReportEntry.Key);
                    jsonWriter.WriteString("status",
                        healthReportEntry.Value.Status.ToString());
                    jsonWriter.WriteString("description",
                        healthReportEntry.Value.Description);
                    jsonWriter.WriteStartObject("data");

                    foreach (var item in healthReportEntry.Value.Data)
                    {
                        jsonWriter.WritePropertyName(item.Key);

                        System.Text.Json.JsonSerializer.Serialize(jsonWriter, item.Value,
                            item.Value?.GetType() ?? typeof(object));
                    }

                    jsonWriter.WriteEndObject();
                    jsonWriter.WriteEndObject();
                }

                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();
            }

            return context.Response.WriteAsync(
                Encoding.UTF8.GetString(memoryStream.ToArray()));
        }
    }
}
