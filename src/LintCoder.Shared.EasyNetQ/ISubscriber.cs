

namespace LintCoder.Shared.EasyNetQ
{
    public interface ISubscriber<T> where T : class
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="message"></param>
        Task SubscribeAsync(T message);
    }
}
