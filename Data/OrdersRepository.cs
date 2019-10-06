using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using ZenStore.Models;

namespace ZenStore.Data
{
    public class OrdersRepository
    {
        private readonly IDbConnection _db;

        public IEnumerable<Order> GetAll()
        {
            return _db.Query<Order>("SELECT * FROM orders;");
        }

        public Order GetById(string id)
        {
            var sql = @"SELECT * FROM orders WHERE id = @id;";

            return _db.QueryFirstOrDefault<Order>(sql, new { id });
        }

        public IEnumerable<Product> GetOrderProducts(string id)
        {
            var sql = @"
            SELECT p.* 
            FROM orders o
            JOIN orderproducts op ON o.id = op.orderid
            JOIN products p ON op.productid = p.id
            WHERE o.id = @id";

            return _db.Query<Product>(sql, new { id });
        }

        public Order Create(Order order)
        {
            var sql = @"
                INSERT INTO orders (id, name, orderin)
                VALUES (@Id, @Name, @OrderIn);";
            _db.Execute(sql, order);

            return order;
        }

        public void CreateOrderProduct(OrderProduct orderProduct)
        {
            var sql = @"
            INSERT INTO orderproducts (id, orderid, productid)
            VALUES (@Id, @OrderId, @ProductId);";
            _db.Execute(sql, orderProduct);
        }

        public Order Edit(Order order)
        {
            var sql = @"
                UPDATE orders SET  
                name = @Name,
                canceled = @Canceled,
                shipped = @Shipped,
                orderin = @OrderIn,
                ordershipped = @OrderShipped,
                ordercanceled = @OrderCanceled
                WHERE id = @Id;";
            _db.Execute(sql, order);

            return order;
        }

        public OrdersRepository(IDbConnection db)
        {
            _db = db;
        }

    }
}