using Microsoft.AspNetCore.Mvc;

using WebApiLab.Bll.Interfaces;
using WebApiLab.Bll.Dtos;
using WebApiLab.Bll.Exceptions;

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
    public ActionResult<IEnumerable<Product>> Get()
    {
        return _productService.GetProducts().ToList();
    }

    // GET api/<ProductController>/5
    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        return _productService.GetProduct(id);
    }

    // POST api/<ProductController>
    [HttpPost]
    public ActionResult<Product> Post([FromBody] Product product)
    {
        var created = _productService.InsertProduct(product);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Product value)
    {
        _productService.UpdateProduct(id, value);
        return NoContent();
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _productService.DeleteProduct(id);
        return NoContent();
    }
}
