

namespace Catalog.Application.Dto
{
    public class ProductDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; }
        public string ShortDescription { get; init; }
        public string? LongDescription { get; init; }
        public string Sku { get; init; }
        public Guid CategoryId { get; init; }
        public string CategoryName { get; init; }
        public DateTime CreateAt { get; init; }

    }
}
