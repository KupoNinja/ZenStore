using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ZenStore.Models;
using ZenStore.Services;

namespace ZenStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersService _os;

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            try
            {
                return Ok(_os.GetOrders());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetById(string id)
        {
            try
            {
                return Ok(_os.GetOrderById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Order> Create([FromBody] Order orderData)
        {
            try
            {
                Order order = _os.AddOrder(orderData);
                return Created("api/orders/" + order.Id, order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Order> EditOrder(string id, [FromBody] Order orderData)
        {
            try
            {
                orderData.Id = id;
                return Ok(_os.EditOrder(orderData));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}/ship")]
        public ActionResult<Order> ShipOrder(string id)
        {
            try
            {
                return Ok(_os.ShipOrder(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}/cancel")]
        public ActionResult<Order> CancelOrder(string id)
        {
            try
            {
                return Ok(_os.CancelOrder(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public OrdersController(OrdersService os)
        {
            _os = os;
        }
    }
}