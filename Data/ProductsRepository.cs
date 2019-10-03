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
            var nRows = _db.Execute(sql, product);

            return product;
        }

        public bool Edit(Product product)
        {
            var nRows = _db.Execute(@"
                UPDATE products SET 
                id = @Id, 
                name = @Name, 
                description = @Description, 
                price = @Price
                WHERE id = @Id;",
                product);

            return nRows == 1;
        }

        public ProductsRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}