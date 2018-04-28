
namespace EcommerceInventory.Persistence
{
    // ReSharper disable once InconsistentNaming
    public interface ISKUStatusRepository
    {
        SKUStatus Get(int sku);    
        SKUStatus Add(SKUStatus status);    
    }
}