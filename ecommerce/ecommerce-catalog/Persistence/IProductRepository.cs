using System.Collections.Generic;
using EcommerceCatalog.Models;

namespace EcommerceCatalog.Persistence
{
    public interface IProductRepository
    {
        ICollection<Product> All();
        Product Get(int sku);
    }
}