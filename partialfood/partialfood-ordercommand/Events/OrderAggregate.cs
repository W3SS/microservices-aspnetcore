using System;
using System.Collections.Generic;

namespace PartialFoods.Services.OrderCommandServer.Events
{
    using System.Linq;

    public class OrderAggregate : IAggregate<OrderEvent>
    {
        private ulong version;
        private readonly string orderId;
        private OrderStatus status;


        public OrderAggregate(string orderId)
        {
            this.orderId = orderId;
            this.status = OrderStatus.None;
        }

        public OrderStatus Status => this.status;

        public string OrderId => this.orderId;

        public ulong Version => this.version;

        public IList<DomainEvent> Accept(OrderRequest request, Dictionary<string, ProductAggregate> productAggregates)
        {
            var evts = new List<DomainEvent>();

            var evt = OrderAcceptedEvent.FromProto(request, this.OrderId);
            evts.Add(evt);

            foreach (var sku in productAggregates.Keys)
            {
                var list = new List<LineItem>();
                foreach (var item in request.LineItems) list.Add(item);
                var quantity = list?.FirstOrDefault(li => li.SKU == sku)?.Quantity;
                evts.AddRange(productAggregates[sku].Reserve(this.orderId, request.UserID, quantity.GetValueOrDefault()));
            }
            return evts;
        }

        public IList<DomainEvent> Cancel(string userId, Dictionary<string, ProductAggregate> productAggregates)
        {
            var evts = new List<DomainEvent>();
            var evt = new OrderCanceledEvent
            {
                OrderId = this.orderId,
                UserId = userId,
                CreatedOn = (ulong)DateTime.UtcNow.Ticks,
                EventId = Guid.NewGuid().ToString(),
            };
            evts.Add(evt);

            foreach (var sku in productAggregates.Keys)
            {
                evts.AddRange(productAggregates[sku].Release(this.orderId, userId, 1)); // TODO: use real quantity
            }

            return evts;
        }

        public void ApplyAll(IList<OrderEvent> evts)
        {
            foreach (var evt in evts)
            {
                this.Apply(evt);
            }
        }

        public void Apply(OrderEvent evt)
        {
            this.version += 1;
            switch (evt)
            {
                case OrderAcceptedEvent _:
                    this.status = OrderStatus.Accepted;
                    break;
                case OrderCanceledEvent _:
                    this.status = OrderStatus.Canceled;
                    break;
            }
        }
    }

    public enum OrderStatus
    {
        None,
        Accepted,
        Canceled,
    }
}