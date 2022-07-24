using Dapr.Client;
using LintCoder.EventBus.Abstractions;
using LintCoder.EventBus.Events;
using Microsoft.Extensions.Logging;

namespace LintCoder.EventBus.Dapr
{
    public class DaprEventBus : IEventBus
    {
        private const string PubSubName = "eshopondapr-pubsub";

        private readonly DaprClient _dapr;
        private readonly ILogger _logger;

        public DaprEventBus(DaprClient dapr, ILogger<DaprEventBus> logger)
        {
            _dapr = dapr;
            _logger = logger;
        }

        public void Publish(IntegrationEvent @event)
        {
            throw new NotImplementedException();
        }

        public async Task PublishAsync(IntegrationEvent integrationEvent)
        {
            var topicName = integrationEvent.GetType().Name;

            _logger.LogInformation(
                "Publishing event {@Event} to {PubsubName}.{TopicName}",
                integrationEvent,
                PubSubName,
                topicName);

            // We need to make sure that we pass the concrete type to PublishEventAsync,
            // which can be accomplished by casting the event to dynamic. This ensures
            // that all event fields are properly serialized.
            await _dapr.PublishEventAsync(PubSubName, topicName, (object)integrationEvent);
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            throw new NotImplementedException();
        }
    }
}