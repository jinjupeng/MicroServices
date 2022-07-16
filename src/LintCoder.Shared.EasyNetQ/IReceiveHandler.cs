using EasyNetQ;

namespace LintCoder.Shared.EasyNetQ
{
    public interface IReceiveHandler
    {
        Guid Id { get; }
        string Queue { get; }

        /// <summary>
        /// 添加消息处理
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        Task ResolveAsync(IReceiveRegistration registration);
    }
}
