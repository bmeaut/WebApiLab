using Microsoft.AspNetCore.Mvc;

using WebApiLab.Bll.Interfaces;
using WebApiLab.Dal.Entities;

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
    public IEnumerable<Product> Get()
    {
        return _productService.GetProducts();
    }

    // GET api/<ProductController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ProductController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
