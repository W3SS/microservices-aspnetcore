using PartialFoods.Services.OrderManagementServer.Entities;
using System.Linq;
using System;

namespace PartialFoods.Services.OrderManagementServer
{
    public class OrderAcceptedEventProcessor
    {
        private readonly IOrderRepository orderRepository;

        public OrderAcceptedEventProcessor(IOrderRepository repository)
        {
            this.orderRepository = repository;
        }

        public bool HandleOrderAcceptedEvent(OrderAcceptedEvent evt)
        {
            Console.WriteLine("Handling order accepted event.");
            var result = this.orderRepository.Add(new Order
            {
                OrderId = evt.OrderId,
                CreatedOn = (long)evt.CreatedOn,
                TaxRate = (int)evt.TaxRate,
                UserId = evt.UserId,
                LineItems = (from itm in evt.LineItems
                             select new Entities.LineItem
                             {
                                 SKU = itm.SKU,
                                 OrderId = evt.OrderId,
                                 Quantity = (int)itm.Quantity,
                                 UnitPrice = (int)itm.UnitPrice
                             }).ToArray()
            });

            return result != null;
        }
    }
}