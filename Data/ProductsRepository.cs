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

        public ProductsRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}