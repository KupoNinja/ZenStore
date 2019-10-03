using System.Collections.Generic;
using ZenStore.Interfaces;

namespace ZenStore.Models
{
    public class Order : IOrder
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public bool Canceled { get; set; }
        public bool Shipped { get; set; }

        public decimal Total { get; }
    }
}