

using Domain.ValueObjects;
using Identity.Domain.Entities;
using Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Efcore.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(u => u.Id).HasConversion(
                id => id.Value,  //writing to database
                value => UserId.Create(value))  //fetching value from database and convert it to valueobject
                .HasColumnName("Id");
            builder.Property(x => x.Email).HasMaxLength(256).HasConversion(email => email.Value, value => Email.Create(value)).HasColumnName("Email").IsRequired();
            builder.Property(x => x.PasswordHash).HasConversion(passwordHash => passwordHash.Value, value => PasswordHash.Create(value)).HasColumnName("PasswordHash").IsRequired();
            builder.Property(x => x.Roles).HasConversion(roles => string.Join(",", roles), value => value.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList()).HasColumnName("Roles").HasMaxLength(800);
            builder.Property(x => x.CreateAt).IsRequired().HasColumnName("CreateAt");
            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
