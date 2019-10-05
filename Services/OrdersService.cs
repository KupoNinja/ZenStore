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
            return _repo.GetAll().ToList();
        }

        public Order GetOrderById(string id)
        {
            var order = _repo.GetById(id);
            if (order == null) { throw new Exception("You're taking empty mind too far. This order doesn't even exist."); }

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

        public Order AddProductsToOrder(Order orderData)
        {
            var order = _repo.GetById(orderData.Id);
            if (order == null) { throw new Exception("You're taking empty mind too far. This order doesn't even exist."); }
            var validOrder = CheckCanceledOrShipped(order);
            validOrder.Name = orderData.Name;
            AddOrderProducts(orderData);

            return _repo.Edit(order);
        }

        public Order ShipOrder(Order orderData)
        {
            var order = _repo.GetById(orderData.Id);
            if (order == null) { throw new Exception("You're taking empty mind too far. This order doesn't even exist."); }
            var validOrder = CheckCanceledOrShipped(order);
            validOrder.OrderShipped = DateTime.Now;
            validOrder.Shipped = true;

            return _repo.Edit(validOrder);
        }

        public Order CancelOrder(Order orderData)
        {
            var order = _repo.GetById(orderData.Id);
            if (order == null) { throw new Exception("You're taking empty mind too far. This order doesn't even exist."); }
            var validOrder = CheckCanceledOrShipped(order);
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