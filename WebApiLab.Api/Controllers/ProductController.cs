using Microsoft.AspNetCore.Mvc;

using WebApiLab.Bll.Dtos;
using WebApiLab.Bll.Interfaces;

namespace WebApiLab.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/<ProductController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAsync()
    {
        return (await _productService.GetProductsAsync()).ToList();
    }

    /// <summary>
    /// Get a specific product with the given identifier
    /// </summary>
    /// <param name="id">Product's identifier</param>
    /// <returns>Returns a specific product with the given identifier</returns>
    /// <response code="200">Listing successful</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetAsync(int id)
    {
        return await _productService.GetProductAsync(id);
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="product">The product to create</param>
    /// <returns>Returns the product inserted</returns>
    /// <response code="201">Insert successful</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Product>> PostAsync([FromBody] Product product)
    {
        var created = await _productService.InsertProductAsync(product);
        return CreatedAtAction(nameof(GetAsync), new { id = created.Id }, created);
    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> PutAsync(int id, [FromBody] Product value)
    {
        await _productService.UpdateProductAsync(id, value);
        return NoContent();
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}
