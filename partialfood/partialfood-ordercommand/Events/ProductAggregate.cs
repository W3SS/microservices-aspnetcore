using System;
using System.Collections.Generic;

namespace PartialFoods.Services.OrderCommandServer.Events
{
    public class ProductAggregate : IAggregate<InventoryEvent>
    {
        private readonly string sku;
        private ulong version;
        private ulong quantity;

        public ProductAggregate(string sku)
        {
            this.sku = sku;
            this.quantity = 0; // initial quantity comes from an event
            this.version = 1;
        }
        public ulong Version => this.version;

        public ulong Quantity => this.quantity;

        public string SKU => this.sku;

        public IList<InventoryEvent> Reserve(string orderId, string userId, uint quantity)
        {
            var evts = new List<InventoryEvent>();

            if (quantity > this.quantity)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Cannot reserve more than on hand quantity");
            }

            var reservedEvent = new InventoryReservedEvent
            {
                OrderId = orderId,
                EventTime = (ulong)DateTime.UtcNow.Ticks,
                SKU = this.sku,
                Quantity = quantity,
                UserId = userId,
                EventId = Guid.NewGuid().ToString()
            };
            evts.Add(reservedEvent);
            return evts;
        }

        public IList<InventoryEvent> Release(string orderId, string userId, uint quantity)
        {
            var evts = new List<InventoryEvent>();

            var releasedEvent = new InventoryReleasedEvent
            {
                SKU = this.sku,
                EventTime = (ulong)DateTime.UtcNow.Ticks,
                Quantity = quantity,
                OrderId = orderId,
                UserId = userId,
                EventId = Guid.NewGuid().ToString()
            };
            evts.Add(releasedEvent);
            return evts;
        }

        public void Apply(InventoryEvent evt)
        {
            this.version += 1;
            switch (evt)
            {
                case InventoryReleasedEvent _:
                    this.quantity += evt.Quantity;
                    break;
                case InventoryReservedEvent _:
                    this.quantity -= evt.Quantity;
                    break;
                case InventoryStockEvent _:
                    this.quantity += evt.Quantity;
                    break;
            }
        }

        public void ApplyAll(IList<InventoryEvent> evts)
        {
            foreach (var evt in evts)
            {
                this.Apply(evt);
            }
        }

        public void ApplyAll(IList<Activity> activities)
        {
            foreach (var activity in activities)
            {
                this.Apply(ToEvent(activity));
            }
        }

        private static InventoryEvent ToEvent(Activity activity)
        {
            InventoryEvent evt = null;

            switch (activity.ActivityType)
            {
                case ActivityType.Released:
                    evt = new InventoryReleasedEvent();
                    break;
                case ActivityType.Reserved:
                    evt = new InventoryReservedEvent();
                    break;
                case ActivityType.Stockadd:
                    evt = new InventoryStockEvent();
                    break;
            }

            if (evt != null)
            {
                evt.SKU = activity.SKU;
                evt.EventTime = activity.Timestamp;
                evt.UserId = "";
                evt.Quantity = activity.Quantity;
                evt.OrderId = activity.OrderID;
                evt.EventId = activity.ActivityID;
            }

            return evt;
        }
    }
}