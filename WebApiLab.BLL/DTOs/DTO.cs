using System;
using System.Collections.Generic;
using WebApiLab.DAL.Entities;

namespace WebApiLab.BLL.DTO
{
    public record Category(int Id, string Name);

    public record Order(int Id, DateTime OrderDate);    

    public record Product
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int UnitPrice { get; init; }
        public ShipmentRegion ShipmentRegion { get; init; }
        public int CategoryId { get; init; }
        public Category Category { get; init; }
        public List<Order> Orders { get; init; }
    }
}