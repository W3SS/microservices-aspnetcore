namespace PartialFoods.Services.InventoryServer.Entities
{
    public class ProductActivity
    {
        public string SKU { get; set; }
        public string ActivityId { get; set; }

        public ActivityType ActivityType { get; set; }

        public long CreatedOn { get; set; }
        public string OrderId { get; set; }

        public int Quantity { get; set; }
    }

    public enum ActivityType
    {
        Reserved = 1,
        Released = 2,
        Shipped = 3,
        StockAdd = 4
    }
}