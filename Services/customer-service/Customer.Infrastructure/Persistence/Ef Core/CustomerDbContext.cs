using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Persistence.Ef_Core;

public class CustomerDbContext:DbContext
{
    public CustomerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Domain.Entities.Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.Entities.Customer>(entity =>
        {
            entity.ToTable("Customer");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LastName)
                .IsRequired().HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(300).IsRequired();
            entity.OwnsOne(e => e.Email, email =>
            {
                email.Property(e => e.Value).HasColumnName("Email").IsRequired().HasMaxLength(50);
            });
            entity.OwnsOne(e => e.PhoneNumber, phone =>
            {
                phone.Property(e => e.Value).HasColumnName("PhoneNumber").HasMaxLength(12);
            });
            entity.OwnsOne(e => e.Address, addr =>
            {
                addr.Property(e => e.City).HasColumnName("City").HasMaxLength(50);
                addr.Property(e => e.Country).HasColumnName("Country").HasMaxLength(50);
                addr.Property(e => e.State).HasColumnName("State").HasMaxLength(100);
                addr.Property(e => e.Street).HasColumnName("Street").HasMaxLength(100);
                addr.Property(e => e.ZipCode).HasColumnName("ZipCode").HasMaxLength(20);
            });
        });
    }
}