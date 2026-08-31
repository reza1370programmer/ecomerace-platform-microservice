

using Catalog.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.OwnsOne(p => p.Price, price =>
            {
                price.Property(p => p.Amount).HasColumnName("Price").HasPrecision(18, 2).IsRequired();
                price.Property(p => p.Currency).HasColumnName("Currency").HasMaxLength(3).IsRequired();
            });
            builder.OwnsOne(p => p.Description, des =>
            {
                des.Property(x => x.ShorDescription).HasColumnName("shorDescriptions").HasMaxLength(200).IsRequired();
                des.Property(x => x.LongDescription).HasColumnName("longDescriptions").HasMaxLength(2000).IsRequired();
            });
            builder.OwnsOne(p => p.Sku, sku =>
            {
                sku.Property(x => x.Value).HasColumnName("Sku").HasMaxLength(100).IsRequired();
            });
            builder.Property(x => x.CategoryId).IsRequired();
            builder.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.CreateAt).IsRequired();
        }
    }
}
