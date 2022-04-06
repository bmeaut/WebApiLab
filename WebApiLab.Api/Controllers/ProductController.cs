﻿using Microsoft.AspNetCore.Mvc;

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

    // GET api/<ProductController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetAsync(int id)
    {
        return await _productService.GetProductAsync(id);
    }

    // POST api/<ProductController>
    [HttpPost]
    public async Task<ActionResult<Product>> PostAsync([FromBody] Product product)
    {
        var created = await _productService.InsertProductAsync(product);
        return CreatedAtAction(nameof(GetAsync), new { id = created.Id }, created);
    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> PutAsync(int id, [FromBody] Product value)
    {
        await _productService.UpdateProductAsync(id, value);
        return NoContent();
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}