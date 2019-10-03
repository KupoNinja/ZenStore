using System.Collections.Generic;
using System.Linq;
using ZenStore.Data;
using ZenStore.Models;

namespace ZenStore.Services
{
    public class ProductsService
    {
        private readonly ProductsRepository _repo;

        public List<Product> GetProducts()
        {
            return _repo.GetAll().ToList();
        }

        // public Product GetProductById()
        // {

        // }

        // public Product AddProduct()
        // {

        // }

        public ProductsService(ProductsRepository repo)
        {
            _repo = repo;
        }
    }
}