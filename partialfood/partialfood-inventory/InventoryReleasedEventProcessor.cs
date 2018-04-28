using System;
using PartialFoods.Services.InventoryServer.Entities;

namespace PartialFoods.Services.InventoryServer
{
    public class InventoryReleasedEventProcessor
    {
        private readonly IInventoryRepository repository;

        public InventoryReleasedEventProcessor(IInventoryRepository repository)
        {
            this.repository = repository;
        }

        public bool HandleInventoryReleasedEvent(InventoryReleasedEvent evt)
        {
            Console.WriteLine($"Handling inventory released event - {evt.EventId}");
            var activity = new ProductActivity
            {
                OrderId = evt.OrderId,
                SKU = evt.SKU,
                Quantity = (int)evt.Quantity,
                ActivityId = evt.EventId,
                CreatedOn = DateTime.UtcNow.Ticks,
                ActivityType = Entities.ActivityType.Released
            };
            var result = this.repository.PutActivity(activity);

            return (result != null);
        }
    }
}