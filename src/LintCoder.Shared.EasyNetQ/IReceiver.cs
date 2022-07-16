

namespace LintCoder.Shared.EasyNetQ
{
    public interface IReceiver<T> where T : class
    {
        /// <summary>
        /// 接收事件
        /// </summary>
        /// <param name="message"></param>
        Task ReceiveAsync(T message);
    }
}
