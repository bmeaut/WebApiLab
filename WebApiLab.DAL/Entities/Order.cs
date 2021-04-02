using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiLab.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; }
                                        = new List<OrderItem>();

        public ICollection<Product> Products { get; }
                                        = new List<Product>();
    }
}
