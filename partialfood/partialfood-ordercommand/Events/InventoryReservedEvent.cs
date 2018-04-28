namespace PartialFoods.Services.OrderCommandServer.Events
{
    public class InventoryReservedEvent : InventoryEvent
    {
        private const string ReservedTopic = "inventoryreserved";

        public override string Topic()
        {
            return ReservedTopic;
        }
    }
}