using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PartialFoods.Services.InventoryServer.Entities;

namespace PartialFoods.Services.InventoryServer
{
    public class InventoryManagementImpl : InventoryManagement.InventoryManagementBase
    {
        private readonly IInventoryRepository repository;

        private readonly ILogger logger;

        public InventoryManagementImpl(IInventoryRepository repository,
            ILogger<InventoryManagementImpl> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public override Task<GetQuantityResponse> GetEffectiveQuantity(GetProductRequest request, Grpc.Core.ServerCallContext context)
        {
            this.logger.LogInformation($"Received query for effective quantity of SKU {request.SKU}");
            var quantity = this.repository.GetCurrentQuantity(request.SKU);
            return Task.FromResult(new GetQuantityResponse { Quantity = (uint)quantity });
        }

        public override Task<ActivityResponse> GetActivity(GetProductRequest request, Grpc.Core.ServerCallContext context)
        {
            this.logger.LogInformation($"Received query for product activity for SKU {request.SKU}");
            var response = new ActivityResponse();
            try
            {
                var activities = this.repository.GetActivity(request.SKU);
                foreach (var activity in activities)
                {
                    response.Activities.Add(new Activity
                    {
                        SKU = activity.SKU,
                        ActivityID = activity.ActivityId,
                        Timestamp = (ulong)activity.CreatedOn,
                        OrderID = activity.OrderId,
                        Quantity = (uint)activity.Quantity,
                        ActivityType = ToProtoActivityType(activity.ActivityType)
                    });
                }
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to retrieve activity for SKU : {}", request.SKU);
                return (Task<ActivityResponse>)Task.FromException(ex);
            }
        }

        private static ActivityType ToProtoActivityType(
            Entities.ActivityType at)
        {
            switch (at)
            {
                case Entities.ActivityType.Released:
                    return ActivityType.Released;
                case Entities.ActivityType.Shipped:
                    return ActivityType.Shipped;
                case Entities.ActivityType.Reserved:
                    return ActivityType.Reserved;
                case Entities.ActivityType.StockAdd:
                    return ActivityType.Stockadd;
                default:
                    return ActivityType.Unknown;
            }
        }
    }
}