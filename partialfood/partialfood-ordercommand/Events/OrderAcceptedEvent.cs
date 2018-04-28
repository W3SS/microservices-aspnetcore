using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PartialFoods.Services.OrderCommandServer.Events
{
    public abstract class OrderEvent : DomainEvent
    {
        [JsonProperty("order_id")]
        public string OrderId;

        [JsonProperty("created_on")]
        public ulong CreatedOn;

        [JsonProperty("user_id")]
        public string UserId;
    }

    public class OrderAcceptedEvent : OrderEvent
    {
        private const string OrdersTopic = "orders";

        public override string Topic()
        {
            return OrdersTopic;
        }

        [JsonProperty("tax_rate")]
        public uint TaxRate;

        [JsonProperty("line_items")]
        public ICollection<EventLineItem> LineItems;

        public static OrderAcceptedEvent FromProto(OrderRequest tx, string orderId)
        {
            var evt = new OrderAcceptedEvent
                          {
                              TaxRate = tx.TaxRate,
                              CreatedOn = tx.CreatedOn,
                              UserId = tx.UserID,
                              EventId = Guid.NewGuid().ToString(),
                              OrderId = orderId,
                              LineItems = new List<EventLineItem>()
                          };
            foreach (var li in tx.LineItems)
            {
                evt.LineItems.Add(new EventLineItem
                {
                    SKU = li.SKU,
                    Quantity = li.Quantity,
                    UnitPrice = li.UnitPrice
                });
            }

            return evt;
        }
    }

    public class EventLineItem
    {
        [JsonProperty("sku")]
        public string SKU;

        [JsonProperty("unit_price")]
        public uint UnitPrice;

        [JsonProperty("quantity")]
        public uint Quantity;
    }
}