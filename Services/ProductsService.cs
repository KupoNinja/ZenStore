using System;
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

        public Product GetProductById(string id)
        {
            var product = _repo.GetById(id);
            if (product == null) { throw new Exception("You're taking empty mind too far. This ID doesn't even exist."); }

            return product;
        }

        public Product AddProduct(Product productData)
        {
            var exists = _repo.GetByName(productData.Name);
            if (exists != null) { throw new Exception("Clear your thoughts. This product already exists."); }

            productData.Id = Guid.NewGuid().ToString();
            _repo.Create(productData);

            return productData;
        }

        public ProductsService(ProductsRepository repo)
        {
            _repo = repo;
        }
    }
}