using Microsoft.Extensions.DependencyInjection;

namespace LintCoder.Shared.EasyNetQ.Service
{
    public interface IEasyNetQServer
    {
        void Register(Action<IServiceCollection> configure);

        Task StartAsync(CancellationToken cancellationToken = default);

        Task StopAsync(CancellationToken cancellationToken = default);

        void Dispose();
    }
}
