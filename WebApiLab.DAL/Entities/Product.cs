using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiLab.Dal.Entities;

public class Product
{
    public int Id { get; set; }

    [Column("ProductName")]
    public string Name { get; set; }

    public int UnitPrice { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<OrderItem> ProductOrders { get; } = new List<OrderItem>();

    public ICollection<Order> Orders { get; } = new List<Order>();

    public ShipmentRegion ShipmentRegion { get; set; }

    public Product(string name)
    {
        Name = name;
    }
}

