using EasyNetQ;

namespace LintCoder.Shared.EasyNetQ.Consumers
{
    public class ReceiveHandler<T> : IReceiveHandler where T : class
    {
        readonly Func<T, Task> onMessage;
        readonly string queue;
        readonly Guid id;

        public ReceiveHandler(Guid id, string queue, Func<T, Task> onMessage)
        {
            this.id = id;
            this.queue = queue;
            this.onMessage = onMessage;
        }

        public string Queue => queue;
        public Guid Id => id;

        public Task ResolveAsync(IReceiveRegistration registration)
        {
            registration.Add<T>(onMessage);
            return Task.CompletedTask;
        }
    }
}
