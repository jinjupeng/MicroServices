

namespace LintCoder.Shared.EasyNetQ.Provider
{
    public interface IConsumerProvider : IDisposable
    {
        /// <summary>
        /// 开始监听消费消息
        /// </summary>
        /// <returns></returns>
        Task ListenAsync();
    }
}
