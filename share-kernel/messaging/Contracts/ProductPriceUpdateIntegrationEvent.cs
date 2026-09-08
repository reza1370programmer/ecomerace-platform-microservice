

namespace Messaging.Contracts
{
    public record ProductPriceUpdateIntegrationEvent
    {
        public Guid ProductId { get; init; }
        public decimal OldPrice { get; init; }
        public decimal NewPrice { get; init; }
        public DateTime UpdateAt { get; init; }
    }
}
