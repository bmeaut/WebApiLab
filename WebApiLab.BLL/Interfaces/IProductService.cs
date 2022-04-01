using WebApiLab.Bll.Dtos;

namespace WebApiLab.Bll.Interfaces;

public interface IProductService
{
    public Task<Product> GetProductAsync(int productId);
    public Task<IEnumerable<Product>> GetProductsAsync();
    public Task<Product> InsertProductAsync(Product newProduct);
    public Task UpdateProductAsync(int productId, Product updatedProduct);
    public Task DeleteProductAsync(int productId);
}
