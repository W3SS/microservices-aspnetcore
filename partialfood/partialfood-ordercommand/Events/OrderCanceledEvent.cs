namespace PartialFoods.Services.OrderCommandServer.Events
{
    public class OrderCanceledEvent : OrderEvent
    {
        private const string CanceledTopic = "canceledorders";

        public override string Topic()
        {
            return CanceledTopic;
        }
    }
}