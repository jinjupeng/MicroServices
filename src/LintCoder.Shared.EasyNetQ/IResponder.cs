

namespace LintCoder.Shared.EasyNetQ
{
    public interface IResponder<TRequest, TResponse>
            where TRequest : class
            where TResponse : class
    {
        /// <summary>
        /// 响应事件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> RespondAsync(TRequest request);
    }
}
