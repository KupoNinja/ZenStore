using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using ZenStore.Models;

namespace ZenStore.Data
{
    public class ProductsRepository
    {
        private readonly IDbConnection _db;

        public IEnumerable<Product> GetAll()
        {
            return _db.Query<Product>("SELECT * FROM products");
        }

        public Product GetById(string id)
        {
            return _db.QueryFirstOrDefault<Product>(
                "SELECT * FROM products WHERE id = @id",
                new { id }
            );
        }

        public Product GetByName(string name)
        {
            return _db.QueryFirstOrDefault<Product>(
                "SELECT * FROM products WHERE name = @name",
                new { name }
            );
        }

        public Product Create(Product product)
        {
            var sql = @"
                INSERT INTO products (id, name, description, price)
                VALUES (@Id, @Name, @Description, @Price);";
            _db.Execute(sql, product);

            return product;
        }

        public Product Edit(Product product)
        {
            var sql = @"
                UPDATE products SET 
                name = @Name, 
                description = @Description, 
                price = @Price
                WHERE id = @Id;";
            _db.Execute(sql, product);

            return product;
        }

        public bool Delete(string id)
        {
            var success = _db.Execute(@"DELETE FROM products WHERE id = @Id", new { id });
            if (success == 1) { return true; }

            return false;
        }

        public ProductsRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}