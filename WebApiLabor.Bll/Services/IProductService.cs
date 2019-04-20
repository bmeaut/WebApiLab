using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLabor.Entities;

namespace WebApiLabor.Bll.Services
{
    public interface IProductService
    {
        Product GetProduct(int productId);
        IEnumerable<Product> GetProducts();
        Product InsertProduct(Product newProduct);
        void UpdateProduct(int productId, Product updatedProduct);
        Task UpdateProductAsync(int productId, Product updatedProduct);
        void DeleteProduct(int productId);
    }
}