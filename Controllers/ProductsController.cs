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
        private readonly ReviewsService _rs;
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

        [HttpGet("{id}/reviews")]
        public ActionResult<Review> GetReviews(string id)
        {
            try
            {
                return Ok(_rs.GetReviewsByProduct(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Product> Create([FromBody] Product productData)
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

        [HttpPut("{id}")]
        public ActionResult<Product> Edit(string id, [FromBody] Product productData)
        {
            try
            {
                productData.Id = id;
                return Ok(_ps.EditProduct(productData));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(string id)
        {
            try
            {
                return Ok(_ps.RemoveProduct(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public ProductsController(ProductsService ps, ReviewsService rs)
        {
            _rs = rs;
            _ps = ps;
        }
    }
}