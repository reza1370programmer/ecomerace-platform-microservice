

namespace Catalog.Domain.DomainEvents
{
    public class ProductCreatedEvent
    {
        public Guid ProductId { get; init; }
        public string Name { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public string Sku { get; init; } = string.Empty;
        public Guid CategoryId { get; init; }
        public DateTime CreateAt { get; set; }

    }
}
