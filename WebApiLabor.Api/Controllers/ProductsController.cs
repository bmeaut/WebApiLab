using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiLabor.Bll.Services;
using WebApiLabor.Api.Dtos; //volt: using WebApiLabor.Entities;

namespace WebApiLabor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
       


        // GET: api/Products
        //[HttpGet]
        //public IEnumerable<Product> Get()
        //{
        //    return _productService.GetProducts();
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<Product>> Get()
        //{
        //    return _productService.GetProducts()
        //        .ToList();
        //}

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return _mapper.Map<List<Product>>(_productService.GetProducts())
                .ToList();
        }

        // GET: api/Products/5
        //[HttpGet("{id}", Name = "Get")]
        //public ActionResult<Product> Get(int id)
        //{
        //    return _productService.GetProduct(id);
        //}

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Product> Get(int id)
        {
            return _mapper.Map<Product>(_productService.GetProduct(id));
        }

        // POST: api/Products
        //[HttpPost]
        //public ActionResult<Product> Post([FromBody] Product product)
        //{
        //    var created = _productService.InsertProduct(product);
        //    return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        //}

        // POST: api/Products
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            var created = _productService
                .InsertProduct(_mapper.Map<Entities.Product>(product));
            return CreatedAtAction(
                        nameof(Get),
                        new { id = created.Id },
                        _mapper.Map<Product>(created)
            );
        }

        // PUT: api/Products/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] Product product)
        //{
        //    _productService.UpdateProduct(id, product);
        //    return NoContent();
        //}

        // PUT: api/Products/5
        //[HttpPut("{id}")]
        //public async IActionResult Put(int id, [FromBody] Product product)
        //{
        //    _productService.
        //        UpdateProduct(id, _mapper.Map<Entities.Product>(product));
        //    return NoContent();
        //}

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            await _productService.
                UpdateProductAsync(id, _mapper.Map<Entities.Product>(product));
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
