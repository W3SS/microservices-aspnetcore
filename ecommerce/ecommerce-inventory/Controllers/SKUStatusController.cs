using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EcommerceInventory.Persistence;

namespace EcommerceInventory.Controllers
{
    [Route("api/skustatus")]
    // ReSharper disable once InconsistentNaming
    public class SKUStatusController : Controller
    {
        private readonly ISKUStatusRepository skuStatusRepository;

        private readonly ILogger<SKUStatusController> logger;

        public SKUStatusController(ISKUStatusRepository skuStatusRepository, ILogger<SKUStatusController> logger)
        {
            this.skuStatusRepository = skuStatusRepository;
            this.logger = logger;
        }

        [HttpGet("{sku}")]
        public IActionResult Get(int sku)
        {
            this.logger.LogInformation("Handling request for SKU " + sku);
            return this.Ok(this.skuStatusRepository.Get(sku));
        }

        [HttpPut("{sku}")]
        public IActionResult Put(int sku, [FromBody]SKUStatus skuStatus)
        {
            return this.Ok(this.skuStatusRepository.Add(skuStatus));
        }
    }
}