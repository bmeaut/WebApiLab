using Microsoft.EntityFrameworkCore;

using WebApiLab.Bll.Interfaces;
using WebApiLab.Dal;
using WebApiLab.Dal.Entities;

namespace WebApiLab.Bll.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public Product GetProduct(int productId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetProducts()
    {
        var products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductOrders)
                .ThenInclude(po => po.Order)
            .ToList();

        return products;
    }

    public Product InsertProduct(Product newProduct)
    {
        throw new NotImplementedException();
    }

    public void UpdateProduct(int productId, Product updatedProduct)
    {
        throw new NotImplementedException();
    }

    public void DeleteProduct(int productId)
    {
        throw new NotImplementedException();
    }
}
