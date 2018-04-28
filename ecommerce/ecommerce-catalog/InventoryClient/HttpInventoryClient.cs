using EcommerceCatalog.Models;
using Steeltoe.Discovery.Client;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace EcommerceCatalog.InventoryClient
{
    public class HttpInventoryClient : IInventoryClient
    {
        private readonly DiscoveryHttpClientHandler handler;
        private const string StockserviceUrlBase = "http://inventory/api/skustatus/";

        public HttpInventoryClient(IDiscoveryClient client)
        {
            this.handler = new DiscoveryHttpClientHandler(client);
        }


        private HttpClient CreateHttpClient()
        {
            return new HttpClient(this.handler, false);
        }

        public async Task<StockStatus> GetStockStatusAsync(int sku)
        {
            StockStatus stockStatus = null;

            using (var client = this.CreateHttpClient())
            {
                var result = await client.GetStringAsync(StockserviceUrlBase + sku);
                stockStatus = JsonConvert.DeserializeObject<StockStatus>(result);
            }

            return stockStatus;
        }
    }
}
