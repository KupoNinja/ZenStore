using System;
using System.Collections.Generic;
using System.Linq;
using ZenStore.Data;
using ZenStore.Models;

namespace ZenStore.Services
{
    public class OrdersService
    {
        private readonly OrdersRepository _repo;

        public List<Order> GetOrders()
        {
            var orders = _repo.GetAll().ToList();
            orders.ForEach(o =>
            {
                var products = _repo.GetOrderProducts(o.Id).ToList();
                o.Products = products;
            });

            return orders;
        }

        public Order GetOrderById(string id)
        {
            var order = _repo.GetById(id);
            if (order == null) { throw new Exception("You're taking empty mind too far. This order doesn't even exist."); }
            var products = _repo.GetOrderProducts(id).ToList();
            order.Products = products;

            return order;
        }

        public Order AddOrder(Order orderData)
        {
            orderData.Id = Guid.NewGuid().ToString();
            orderData.OrderIn = DateTime.Now;
            _repo.Create(orderData);
            AddOrderProducts(orderData);

            return orderData;
        }

        public Order EditOrder(Order orderData)
        {
            var order = _repo.GetById(orderData.Id);
            if (order == null) { throw new Exception("You're taking empty mind too far. This order doesn't even exist."); }
            var validOrder = CheckCanceledOrShipped(order);
            validOrder.Name = orderData.Name;
            validOrder.Products = orderData.Products;
            validOrder.Canceled = false;
            validOrder.Shipped = false;
            validOrder.OrderShipped = null;
            validOrder.OrderCanceled = null;
            AddOrderProducts(validOrder);

            return _repo.Edit(validOrder);
        }

        public Order ShipOrder(string id)
        {
            var order = _repo.GetById(id);
            var validOrder = CheckCanceledOrShipped(order);
            var products = _repo.GetOrderProducts(id).ToList();
            validOrder.Products = products;
            validOrder.OrderShipped = DateTime.Now;
            validOrder.Shipped = true;

            return _repo.Edit(validOrder);
        }

        public Order CancelOrder(string id)
        {
            var order = _repo.GetById(id);
            var validOrder = CheckCanceledOrShipped(order);
            var products = _repo.GetOrderProducts(id).ToList();
            validOrder.Products = products;
            validOrder.OrderCanceled = DateTime.Now;
            validOrder.Canceled = true;

            return _repo.Edit(validOrder);
        }

        public Order CheckCanceledOrShipped(Order orderData)
        {
            if (orderData.Shipped || orderData.Canceled) { throw new Exception("This order has come and gone like thoughts in the wind. You cannot change it."); }
            return orderData;
        }

        public void AddOrderProducts(Order orderData)
        {
            var orderProduct = new OrderProduct();
            orderProduct.OrderId = orderData.Id;

            orderData.Products.ForEach(p =>
            {
                orderProduct.Id = Guid.NewGuid().ToString();
                orderProduct.ProductId = p.Id;
                _repo.CreateOrderProduct(orderProduct);
            });
        }

        public OrdersService(OrdersRepository repo)
        {
            _repo = repo;
        }
    }
}