using Newtonsoft.Json;

namespace PartialFoods.Services.OrderCommandServer.Events
{
    public abstract class DomainEvent
    {
        [JsonProperty("event_id")]
        public string EventId { get; set; }

        public abstract string Topic();
    }

    public interface IEventEmitter
    {
        bool Emit(DomainEvent evt);

        /*bool EmitOrderAcceptedEvent(OrderAcceptedEvent evt);
        bool EmitInventoryReservedEvent(InventoryReservedEvent evt);

        bool EmitOrderCanceledEvent(OrderCanceledEvent evt);
        bool EmitInventoryReleasedEvent(InventoryReleasedEvent evt); */
    }
}