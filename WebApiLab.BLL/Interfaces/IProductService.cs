using WebApiLab.Bll.Dtos;

namespace WebApiLab.Bll.Interfaces;

public interface IProductService
{
    public Product GetProduct(int productId);
    public IEnumerable<Product> GetProducts();
    public Product InsertProduct(Product newProduct);
    public Task UpdateProductAsync(int productId, Product updatedProduct);
    public void DeleteProduct(int productId);
}
