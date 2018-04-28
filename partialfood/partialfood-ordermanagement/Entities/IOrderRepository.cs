namespace PartialFoods.Services.OrderManagementServer.Entities
{
    public interface IOrderRepository
    {
        Order Add(Order order);
        Order GetOrder(string orderId);
        OrderActivity AddActivity(OrderActivity activity);

        bool OrderExists(string orderId);
    }
}