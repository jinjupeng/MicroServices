using Microsoft.Extensions.DependencyInjection;

namespace LintCoder.Shared.EasyNetQ.Infrastructure
{
    public interface IConsumerBuilder
    {
        IServiceCollection Services { get; }

        /// <summary>
        /// 添加接收消息处理事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        IConsumerBuilder AddReceiver<T>(string queue, Func<IServiceProvider, T, Task> onMessage) where T : class;
        /// <summary>
        /// 添加响应消息处理事件
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="responder"></param>
        /// <returns></returns>
        IConsumerBuilder AddResponder<TRequest, TResponse>(Func<IServiceProvider, TRequest, Task<TResponse>> responder)
            where TRequest : class
            where TResponse : class;
        /// <summary>
        /// 添加订阅消息处理事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subscriptionId"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        IConsumerBuilder AddSubscriber<T>(string subscriptionId, Func<IServiceProvider, T, Task> onMessage) where T : class;

    }
}
