using CSRedis;
using Polly;
using Polly.Extensions.Http;

namespace LintCoder.Identity.API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class RedisPollyExtensions
    {
        /// <summary>
        /// redis连接重试扩展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisPollyHandler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("redis", _ =>
            {
                _.BaseAddress = new Uri(configuration.GetConnectionString("CSRedisConnection"));
            }).AddPolicyHandler(GetRetryPolicy());

            return services;
        }

        /// <summary>
        /// 重试策略
        /// </summary>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var retryCount = 3;
            var retrySleepDuration = 2;

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<RedisClientException>()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable || msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(
                    retryCount,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(retrySleepDuration, retryAttempt)),
                    onRetryAsync: (_, _) =>
                    {
                        Console.WriteLine("GetRetryPolicy retrying...");
                        return Task.CompletedTask;
                    });
        }
    }
}
