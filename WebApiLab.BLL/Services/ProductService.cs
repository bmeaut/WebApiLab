using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using WebApiLab.Bll.Dtos;
using WebApiLab.Bll.Exceptions;
using WebApiLab.Bll.Interfaces;
using WebApiLab.Dal;

namespace WebApiLab.Bll.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Product GetProduct(int productId)
    {
        return _context.Products
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .SingleOrDefault(p => p.Id == productId)
            ?? throw new EntityNotFoundException("Nem található a termék");
    }

    public IEnumerable<Product> GetProducts()
    {
        var products = _context.Products
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .AsEnumerable();

        return products;
    }

    public Product InsertProduct(Product newProduct)
    {
        var efProduct = _mapper.Map<Dal.Entities.Product>(newProduct);
        _context.Products.Add(efProduct);
        _context.SaveChanges();
        return GetProduct(efProduct.Id);
    }

    public void UpdateProduct(int productId, Product updatedProduct)
    {
        var efProduct = _mapper.Map<Dal.Entities.Product>(updatedProduct);
        efProduct.Id = productId;
        var entry = _context.Attach(efProduct);
        entry.State = EntityState.Modified;
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (_context.Products.SingleOrDefault(p => p.Id == productId) == null)
                throw new EntityNotFoundException("Nem található a termék");
            else
                throw;
        }
    }

    public void DeleteProduct(int productId)
    {
        _context.Products.Remove(new Dal.Entities.Product { Id = productId });
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (_context.Products.SingleOrDefault(p => p.Id == productId) == null)
                throw new EntityNotFoundException("Nem található a termék");
            else
                throw;
        }
    }
}
