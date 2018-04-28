using System.Linq;
using System.Threading.Tasks;

using grpc = Grpc.Core;
using PartialFoods.Services.OrderManagementServer.Entities;

namespace PartialFoods.Services.OrderManagementServer
{
    public class OrderManagementImpl : OrderManagement.OrderManagementBase
    {
        private readonly IOrderRepository repository;

        public OrderManagementImpl(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public override Task<OrderExistsResponse> OrderExists(GetOrderRequest request, grpc::ServerCallContext context)
        {
            var exists = this.repository.OrderExists(request.OrderID);
            var resp = new OrderExistsResponse
            {
                Exists = exists,
                OrderID = request.OrderID
            };
            return Task.FromResult(resp);
        }
        public override Task<GetOrderResponse> GetOrder(GetOrderRequest request, grpc::ServerCallContext context)
        {
            var response = new GetOrderResponse();

            var order = this.repository.GetOrder(request.OrderID);
            if (order != null)
            {
                response.OrderID = order.OrderId;
                response.CreatedOn = (ulong)order.CreatedOn;
                response.TaxRate = (uint)order.TaxRate;
                foreach (var li in order.LineItems)
                {
                    response.LineItems.Add(new LineItem
                    {
                        SKU = li.SKU,
                        Quantity = (uint)li.Quantity,
                        UnitPrice = (uint)li.UnitPrice,
                    });
                }
                var status = OrderStatus.Open;
                if (order.Activities.FirstOrDefault(a => a.ActivityType == ActivityType.Canceled) != null)
                {
                    status = OrderStatus.Canceled;
                }
                response.Status = status;
            }

            return Task.FromResult(response);
        }
    }
}