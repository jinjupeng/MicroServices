

namespace LintCoder.Shared.EasyNetQ
{
    public interface IBusClientFactory
    {
        /// <summary>
        /// 创建总线对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBusClient Create(string name);
    }
}
