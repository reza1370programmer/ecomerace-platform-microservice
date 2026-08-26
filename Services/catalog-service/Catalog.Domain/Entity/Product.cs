

using Catalog.Domain.ValueObject;
using Domain.ValueObjects;

namespace Catalog.Domain.Entity
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Money Price { get; private set; } = null;
        public ProductDescription Description { get; private set; }
        public SKU Sku { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; set; }
        public DateTime CreateAt { get; private set; }
        public DateTime? UpdateAt { get; private set; }

        private Product() { }

        public Product(string name, Money price, ProductDescription description, SKU sku, Guid categoryId)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("product name cannot be empty");
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Description = description;
            Sku = sku;
            CategoryId = categoryId;
            CreateAt = DateTime.UtcNow;
        }
        public static Product Create(string name, Money price, ProductDescription description, SKU sku, Guid categoryId)
        {
            return new Product(name, price, description, sku, categoryId);
        }
        public void PriceUpdate(Money price)
        {
            Price = price;
            UpdateAt = DateTime.UtcNow;
        }
        public void DescriptionUpdate(ProductDescription description)
        {
            Description = description;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
