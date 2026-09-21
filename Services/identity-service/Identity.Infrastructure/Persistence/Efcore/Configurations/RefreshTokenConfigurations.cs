

using Identity.Domain.Entities;
using Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Efcore.Configurations
{
    public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.HasKey(x => x.Id);
            builder.Property(u => u.UserId).HasConversion(
              id => id.Value,  //writing to database
              value => UserId.Create(value))  //fetching value from database and convert it to valueobject
              .HasColumnName("UserId");
            builder.Property(x => x.Token).IsRequired().HasMaxLength(900);
        }
    }
}
