

namespace Catalog.Domain.DomainEvents
{
    public class ProductPriceUpdatedEvent
    {
        public Guid ProductId { get; init; }
        public decimal OldPrice { get; init; }
        public decimal NewPrice { get; init; }
        public DateTime UpdateAt { get; init; }
    }
}
