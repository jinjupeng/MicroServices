using LintCoder.Shared.EasyNetQ.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace LintCoder.Shared.EasyNetQ.Producers
{
    public class DefaultBusClientFactory : IBusClientFactory
    {
        ConcurrentDictionary<string, IBusClient> busDictionary;
        readonly IServiceProvider serviceProvider;

        public DefaultBusClientFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.busDictionary = new ConcurrentDictionary<string, IBusClient>();
        }

        /// <summary>
        /// 创建总线对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBusClient Create(string name)
        {
            var optionsFactory = serviceProvider.GetService<IOptionsFactory<ProducerOptions>>();
            var rabbitProducerOptions = optionsFactory.Create(name);
            if ((rabbitProducerOptions.Hosts == null || rabbitProducerOptions.Hosts.Length == 0) && string.IsNullOrEmpty(rabbitProducerOptions.ConnectionString))
            {
                throw new InvalidOperationException($"{nameof(BaseOptions)} named '{name}' is not configured");
            }

            lock (busDictionary)
            {
                if (!busDictionary.TryGetValue(name, out IBusClient busClient) || busClient == null || !busClient.IsConnected)
                {
                    busClient = new BusClient(rabbitProducerOptions);
                    busDictionary.AddOrUpdate(name, busClient, (n, b) => busClient);
                }
                return busClient;
            }
        }

    }
}
