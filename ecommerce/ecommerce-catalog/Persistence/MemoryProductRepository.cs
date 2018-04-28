using System.Collections.Generic;
using EcommerceCatalog.Models;

namespace EcommerceCatalog.Persistence
{
    public class MemoryProductRepository : IProductRepository
    {
        private readonly Dictionary<int, Product> products;

        public MemoryProductRepository()
        {
            this.products = new Dictionary<int, Product>
                                {
                                    { 123, new Product { SKU = 123, Name = "The Magic 123" } },
                                    { 456, new Product { SKU = 456, Name = "Supervac" } }
                                };


        }
        public ICollection<Product> All()
        {
            return this.products.Values;
        }

        public Product Get(int sku)
        {
            return this.products[sku];
        }
    }
}
