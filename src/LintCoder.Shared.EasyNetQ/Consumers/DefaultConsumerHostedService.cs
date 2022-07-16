using LintCoder.Shared.EasyNetQ.Provider;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LintCoder.Shared.EasyNetQ.Consumers
{
    public class DefaultConsumerHostedService : IHostedService
    {
        ILoggerFactory loggerFactory;
        IEnumerable<IConsumerProvider> easyNetQConsumerProviders;
        public DefaultConsumerHostedService(ILoggerFactory loggerFactory, IEnumerable<IConsumerProvider> easyNetQConsumerProviders)
        {
            this.easyNetQConsumerProviders = easyNetQConsumerProviders;
            this.loggerFactory = loggerFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var logger = loggerFactory.CreateLogger<DefaultConsumerHostedService>();

            foreach (var provider in easyNetQConsumerProviders)
            {
                await provider.ListenAsync();

                logger.LogInformation($"Consumer Listen:{provider}");
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() =>
            {
                var logger = loggerFactory.CreateLogger<DefaultConsumerHostedService>();

                foreach (var provider in easyNetQConsumerProviders)
                {
                    provider.Dispose();

                    logger.LogInformation($"Consumer Stoped:{provider}");
                }
            });
            await Task.CompletedTask;
        }
    }
}
