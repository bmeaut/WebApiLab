using WebApiLab.Dal.Entities;

namespace WebApiLab.Bll.Dtos
{
    public record Category(int Id, string Name);

    public record Order(int Id, DateTime OrderDate);

    public record Product
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public int UnitPrice { get; init; }
        public ShipmentRegion ShipmentRegion { get; init; }
        public int CategoryId { get; init; }
        public Category? Category { get; init; }
        public List<Order>? Orders { get; init; }
    }
}