

using Catalog.Application.Interfaces;
using MassTransit;

namespace Catalog.Infrastructure.EventBus
{
    
    public class EventBusService : IEventBusService
    {
        public readonly IPublishEndpoint _publishEndpoint;

        public EventBusService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            await _publishEndpoint.Publish<T>(message, cancellationToken);
        }
    }
}
