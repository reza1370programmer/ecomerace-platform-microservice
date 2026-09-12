

namespace Messaging.Contracts
{
    public record ProductCreateIntegrationEvent
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string Sku { get; init; }
        public Guid CategoryId { get; init; }
        public DateTime CreateAt { get; init; }

    }
}
