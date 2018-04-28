using Microsoft.AspNetCore.Mvc;
using EcommerceCatalog.InventoryClient;
using EcommerceCatalog.Persistence;

namespace EcommerceCatalog.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IInventoryClient inventoryClient;

        public ProductsController(IProductRepository productRepository, IInventoryClient inventoryClient)
        {
            this.productRepository = productRepository;
            this.inventoryClient = inventoryClient;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return this.Ok(this.productRepository.All());
        }

        [HttpGet("{sku}")]
        public IActionResult GetProduct(int sku)
        {
            var product = new
            {
                Product = this.productRepository.Get(sku),
                Status = this.inventoryClient.GetStockStatusAsync(sku).Result
            };
            return this.Ok(product);
        }
    }
}