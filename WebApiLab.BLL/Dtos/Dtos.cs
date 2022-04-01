using System.ComponentModel.DataAnnotations;

using WebApiLab.Dal.Entities;

namespace WebApiLab.Bll.Dtos
{
    public record Category(int Id, string Name);

    public record Order(int Id, DateTime OrderDate);

    public record Product
    {
        public int Id { get; init; }
        [Required(ErrorMessage = "Product name is required.", AllowEmptyStrings = false)]
        public string Name { get; init; } = null!;
        [Range(1, int.MaxValue, ErrorMessage = "Unit price must be higher than 0.")]
        public int UnitPrice { get; init; }
        public ShipmentRegion ShipmentRegion { get; init; }
        public int CategoryId { get; init; }
        public Category? Category { get; init; }
        public List<Order>? Orders { get; init; }
    }
}