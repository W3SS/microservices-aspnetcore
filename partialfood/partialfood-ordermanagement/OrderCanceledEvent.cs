using Newtonsoft.Json;

namespace PartialFoods.Services.OrderManagementServer
{
    public class OrderCanceledEvent
    {
        [JsonProperty("created_on")]
        public ulong CreatedOn { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("event_id")]
        public string EventId { get; set; }
    }
}