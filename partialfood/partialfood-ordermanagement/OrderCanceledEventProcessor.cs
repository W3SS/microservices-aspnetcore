using PartialFoods.Services.OrderManagementServer.Entities;
using System;

namespace PartialFoods.Services.OrderManagementServer
{
    public class OrderCanceledEventProcessor
    {
        private readonly IOrderRepository orderRepository;

        public OrderCanceledEventProcessor(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public bool HandleOrderCanceledEvent(OrderCanceledEvent orderCanceledEvent)
        {
            Console.WriteLine("Handling order canceled event");

            var result = this.orderRepository.AddActivity(new OrderActivity
            {
                OccuredOn = (long)orderCanceledEvent.CreatedOn,
                ActivityId = orderCanceledEvent.EventId,
                UserId = orderCanceledEvent.UserId,
                OrderId = orderCanceledEvent.OrderId,
                ActivityType = ActivityType.Canceled
            });
            return result != null;
        }
    }
}