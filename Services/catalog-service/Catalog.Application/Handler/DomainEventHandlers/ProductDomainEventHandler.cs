

using Catalog.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handler.DomainEventHandlers
{
    public class ProductDomainEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        public readonly ILogger<ProductDomainEventHandler> _logger;

        public ProductDomainEventHandler(ILogger<ProductDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"product with id {notification.ProductId} is created");
            return Task.CompletedTask;
        }
    }
}
