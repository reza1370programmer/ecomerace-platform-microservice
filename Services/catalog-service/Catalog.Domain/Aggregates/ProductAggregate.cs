

using Catalog.Domain.DomainEvents;
using Catalog.Domain.Entity;
using Catalog.Domain.ValueObject;
using Domain.ValueObjects;

namespace Catalog.Domain.Aggregates
{
    /// <summary>
    /// this is aggregate root class in this class we should add or update product and handle domain events 
    /// and other services should access this class not product directly
    /// </summary>
    public class ProductAggregate
    {
        public Product Product { get; private set; } = null;
        private readonly List<object> _domainEvents = new();
        public IReadOnlyCollection<object> DomainEvents => _domainEvents;


        private ProductAggregate() { }

        public ProductAggregate(Product product)
        {
            Product = product;
            AddDomainEvents(new ProductCreatedEvent
            {
                ProductId = product.Id,
                CategoryId = product.CategoryId,
                CreateAt = product.CreateAt,
                Name = product.Name,
                Price = product.Price.Amount,
                Sku = product.Sku.Value
            });
        }

        public static ProductAggregate CreateProduct(string name, Money price, ProductDescription description, SKU sku, Guid categoryId)
        {
            var product = Product.Create(name, price, description, sku, categoryId);
            return new ProductAggregate(product);
        }
        public void UpdateProductPrice(Money price)
        {
            var oldPrice = Product.Price;
            Product.PriceUpdate(price);
            AddDomainEvents(new ProductPriceUpdatedEvent
            {
                NewPrice = price.Amount,
                OldPrice = oldPrice.Amount,
                ProductId = Product.Id,
                UpdateAt = DateTime.UtcNow
            });
        }
        public void ClearDomainEvents() => _domainEvents.Clear();

        public void AddDomainEvents(object eventItem)
        {
            _domainEvents.Add(eventItem);
        }
    }
}
