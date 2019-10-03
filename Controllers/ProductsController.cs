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
                return _ps.GetProducts();
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