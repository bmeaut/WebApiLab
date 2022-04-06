using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using WebApiLab.Dal.Entities;

namespace WebApiLab.Dal;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .HasMaxLength(15)
            .IsRequired();

        modelBuilder.Entity<Category>().HasData(
            new Category("Ital") { Id = 1 }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product("Sör") { Id = 1, UnitPrice = 50, CategoryId = 1, ShipmentRegion = ShipmentRegion.Asia },
            new Product("Bor") { Id = 2, UnitPrice = 550, CategoryId = 1 },
            new Product("Tej") { Id = 3, UnitPrice = 260, CategoryId = 1 },
            new Product("Whiskey")
            {
                Id = 4,
                UnitPrice = 960,
                CategoryId = 1,
                ShipmentRegion = ShipmentRegion.Australia
            },
            new Product("Rum")
            {
                Id = 5,
                UnitPrice = 960,
                CategoryId = 1,
                ShipmentRegion = ShipmentRegion.Eu | ShipmentRegion.NorthAmerica
            }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, OrderDate = new DateTime(2019, 02, 01) }
        );

        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem { Id = 1, OrderId = 1, ProductId = 1 },
            new OrderItem { Id = 2, OrderId = 1, ProductId = 2 }
        );

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Orders)
            .WithMany(o => o.Products)
            .UsingEntity<OrderItem>(
                j => j
                    .HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Restrict),
                j => j
                    .HasOne(oi => oi.Product)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(oi => oi.ProductId),
                j =>
                {
                    j.HasKey(oi => oi.Id);
                });

        modelBuilder
            .Entity<Product>()
            .Property(e => e.ShipmentRegion)
            .HasConversion(new EnumToStringConverter<ShipmentRegion>());
    }


    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders => Set<Order>();
}
