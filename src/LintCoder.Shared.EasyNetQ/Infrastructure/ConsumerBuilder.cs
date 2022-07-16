using LintCoder.Shared.EasyNetQ.Consumers;
using LintCoder.Shared.EasyNetQ.Infrastructure.Internal;
using LintCoder.Shared.EasyNetQ.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace LintCoder.Shared.EasyNetQ.Infrastructure
{
    public class ConsumerBuilder : IConsumerBuilder
    {
        ConsumerOptions easyNetQConsumerOptions;
        Guid guid = Guid.NewGuid();
        bool receiveConsumerProviderIsRegister = false;

        public ConsumerBuilder(IServiceCollection services, ConsumerOptions easyNetQConsumerOptions)
        {
            this.Services = services;
            this.easyNetQConsumerOptions = easyNetQConsumerOptions;
        }

        public IServiceCollection Services { get; private set; }

        /// <summary>
        /// 添加接收消息处理事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        public IConsumerBuilder AddReceiver<T>(string queue, Func<IServiceProvider, T, Task> onMessage) where T : class
        {
            queue = string.IsNullOrEmpty(queue) ? easyNetQConsumerOptions.Queue : queue;

            if (string.IsNullOrEmpty(queue))
            {
                throw new ArgumentException($"queue cann't be empty", nameof(queue));
            }

            if (!receiveConsumerProviderIsRegister)
            {
                receiveConsumerProviderIsRegister = true;
                Services.AddSingleton<IConsumerProvider>(serviceProvider =>
                {
                    var handlers = serviceProvider.GetService<IEnumerable<IReceiveHandler>>();
                    return new ReceiveConsumerProvider(easyNetQConsumerOptions, handlers.Where(f => f.Id == guid).ToArray());
                });
            }
            Services.AddSingleton<IReceiveHandler>(serviceProvider =>
            {
                return new ReceiveHandler<T>(guid, queue, async t =>
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        await onMessage?.Invoke(scope.ServiceProvider, t);
                    }
                });
            });
            return this;
        }
        /// <summary>
        /// 添加响应消息处理事件
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="responder"></param>
        /// <returns></returns>
        public IConsumerBuilder AddResponder<TRequest, TResponse>(Func<IServiceProvider, TRequest, Task<TResponse>> responder)
            where TRequest : class
            where TResponse : class
        {
            Services.AddSingleton<IConsumerProvider>(serviceProvider =>
            {
                return new RespondConsumerProvider<TRequest, TResponse>(easyNetQConsumerOptions, async request =>
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        return await responder.Invoke(scope.ServiceProvider, request);
                    }
                });
            });

            return this;
        }
        /// <summary>
        /// 添加订阅消息处理事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subscriptionId"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        public IConsumerBuilder AddSubscriber<T>(string subscriptionId, Func<IServiceProvider, T, Task> onMessage) where T : class
        {
            Services.AddSingleton<IConsumerProvider>(serviceProvider =>
            {
                return new SubscribeConsumerProvider<T>(easyNetQConsumerOptions, subscriptionId, async result =>
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        await onMessage?.Invoke(scope.ServiceProvider, result);
                    }
                });
            });

            return this;
        }
    }
}
