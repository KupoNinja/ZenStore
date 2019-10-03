using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ZenStore.Models;
using ZenStore.Services;

namespace ZenStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _ps;

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(_ps.GetProducts());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(string id)
        {
            try
            {
                return Ok(_ps.GetProductById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product productData)
        {
            try
            {
                Product product = _ps.AddProduct(productData);
                return Created("api/products/" + product.Id, product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public ProductsController(ProductsService ps)
        {
            _ps = ps;
        }
    }
}