using System;
using PartialFoods.Services.InventoryServer.Entities;

namespace PartialFoods.Services.InventoryServer
{
    public class InventoryReservedEventProcessor
    {
        private readonly IInventoryRepository repository;

        public InventoryReservedEventProcessor(IInventoryRepository repository)
        {
            this.repository = repository;
        }

        public bool HandleInventoryReservedEvent(InventoryReservedEvent evt)
        {
            Console.WriteLine($"Handling inventory reserved event - {evt.EventId}");
            ProductActivity activity = new ProductActivity
            {
                OrderId = evt.OrderId,
                SKU = evt.SKU,
                Quantity = (int)evt.Quantity,
                ActivityId = evt.EventId,
                CreatedOn = DateTime.UtcNow.Ticks,
                ActivityType = Entities.ActivityType.Reserved
            };
            var result = this.repository.PutActivity(activity);

            return result != null;
        }
    }
}