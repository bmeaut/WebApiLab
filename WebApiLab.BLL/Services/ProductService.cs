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

    public async Task<Product> GetProductAsync(int productId)
    {
        return await _context.Products
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(p => p.Id == productId)
            ?? throw new EntityNotFoundException("Nem található a termék");
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var products = await _context.Products
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return products;
    }

    public async Task<Product> InsertProductAsync(Product newProduct)
    {
        var efProduct = _mapper.Map<Dal.Entities.Product>(newProduct);
        _context.Products.Add(efProduct);
        await _context.SaveChangesAsync();
        return await GetProductAsync(efProduct.Id);
    }

    public async Task UpdateProductAsync(int productId, Product updatedProduct)
    {
        var efProduct = _mapper.Map<Dal.Entities.Product>(updatedProduct);
        efProduct.Id = productId;
        var entry = _context.Attach(efProduct);
        entry.State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Products.AnyAsync(p => p.Id == productId))
                throw new EntityNotFoundException("Nem található a termék");
            else
                throw;
        }
    }

    public async Task DeleteProductAsync(int productId)
    {
        _context.Products.Remove(new Dal.Entities.Product(null!) { Id = productId });
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Products.AnyAsync(p => p.Id == productId))
                throw new EntityNotFoundException("Nem található a termék");
            else
                throw;
        }
    }
}
