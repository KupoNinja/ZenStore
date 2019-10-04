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
            return _db.Query<Order>("SELECT * FROM orders");
        }

        public Order GetById(string id)
        {
            return _db.QueryFirstOrDefault<Order>(
                "SELECT * FROM order WHERE id = @id",
                new { id }
            );
        }

        //NOTE Get Products in here
        public Order Create(Order order)
        {
            var sql = @"
                INSERT INTO orders (id, name, description, rating, productid)
                VALUES (@Id, @Name, @Description, @Rating, @ProductId);";
            var nRows = _db.Execute(sql, order);

            return order;
        }

        //NOTE Get Products in here
        public Order Edit(Order order)
        {
            var nRows = _db.Execute(@"
                UPDATE orders SET 
                id = @Id, 
                name = @Name, 
                description = @Description, 
                rating = @Rating
                WHERE id = @Id;",
                order);

            return order;
        }

        public OrdersRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}