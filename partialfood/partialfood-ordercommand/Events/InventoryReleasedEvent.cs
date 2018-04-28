using Newtonsoft.Json;

namespace PartialFoods.Services.OrderCommandServer.Events
{
    public abstract class InventoryEvent : DomainEvent
    {
        [JsonProperty("sku")]
        public string SKU { get; set; }

        [JsonProperty("quantity")]
        public uint Quantity { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("event_time")]
        public ulong EventTime { get; set; }

    }

    public class InventoryReleasedEvent : InventoryEvent
    {
        private const string ReleasedTopic = "inventoryreleased";

        public override string Topic()
        {
            return ReleasedTopic;
        }
    }

    public class InventoryStockEvent : InventoryEvent
    {
        private const string StockTopic = "inventorystock";

        public override string Topic()
        {
            return StockTopic;
        }
    }
}