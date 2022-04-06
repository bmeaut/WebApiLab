namespace WebApiLab.Dal.Entities;

public class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public ICollection<Product> Products { get; } = new List<Product>();
}
