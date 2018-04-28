using EcommerceCatalog.Models;
using System.Threading.Tasks;

namespace EcommerceCatalog.InventoryClient
{
    public interface IInventoryClient
    {
        Task<StockStatus> GetStockStatusAsync(int sku);
    }
}