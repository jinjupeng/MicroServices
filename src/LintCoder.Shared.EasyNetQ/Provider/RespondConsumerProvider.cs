using EasyNetQ;
using LintCoder.Shared.EasyNetQ.Infrastructure.Internal;

namespace LintCoder.Shared.EasyNetQ.Provider
{
    public class RespondConsumerProvider<TRequest, TResponse> : IConsumerProvider
        where TRequest : class
        where TResponse : class
    {
        readonly Func<TRequest, Task<TResponse>> onMessageRecieved;
        IBus bus;
        readonly ConsumerOptions easyNetQConsumerOptions;
        IDisposable result;
        bool disposed = false;

        public RespondConsumerProvider(ConsumerOptions easyNetQConsumerOptions, Func<TRequest, Task<TResponse>> onMessageRecieved)
        {
            this.onMessageRecieved = onMessageRecieved;
            this.easyNetQConsumerOptions = easyNetQConsumerOptions;
            this.bus = easyNetQConsumerOptions.CreateBus();
        }

        public async Task ListenAsync()
        {
            CheckDisposed();

            if (result == null)
            {
                result = await bus.Rpc.RespondAsync<TRequest, TResponse>(async (t, _) =>
                {
                    return await onMessageRecieved?.Invoke(t);
                }, cfg =>
                {
                    if (!string.IsNullOrEmpty(easyNetQConsumerOptions.Queue))
                    {
                        cfg.WithQueueName(easyNetQConsumerOptions.Queue);
                    }
                    if (easyNetQConsumerOptions.Durable != null)
                    {
                        cfg.WithDurable(easyNetQConsumerOptions.Durable.Value);
                    }
                    if (easyNetQConsumerOptions.PrefetchCount > 0)
                    {
                        cfg.WithPrefetchCount(easyNetQConsumerOptions.PrefetchCount);
                    }
                });
            }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                result?.Dispose();
                bus?.Dispose();
            }
        }
        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        public override string ToString()
        {
            return $"{typeof(TRequest).Name}=>{typeof(TResponse).Name},IsConnected-{bus?.Advanced?.IsConnected ?? false}";
        }
    }
}
