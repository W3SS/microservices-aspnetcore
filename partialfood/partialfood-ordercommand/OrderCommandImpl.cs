using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using grpc = Grpc.Core;

// required for byte array extension
using PartialFoods.Services.OrderCommandServer.Events;
using Microsoft.Extensions.Logging;

namespace PartialFoods.Services.OrderCommandServer
{
    public class OrderCommandImpl : OrderCommand.OrderCommandBase
    {
        private readonly IEventEmitter eventEmitter;
        private readonly OrderManagement.OrderManagementClient orderManagementClient;
        private readonly InventoryManagement.InventoryManagementClient inventoryManagementClient;
        private readonly ILogger logger;

        public OrderCommandImpl(
            ILogger logger,
            IEventEmitter emitter,
         OrderManagement.OrderManagementClient orderManagementClient,
         InventoryManagement.InventoryManagementClient inventoryManagementClient)
        {
            this.logger = logger;
            this.eventEmitter = emitter;
            this.orderManagementClient = orderManagementClient;
            this.inventoryManagementClient = inventoryManagementClient;
        }

        public override Task<CancelOrderResponse> CancelOrder(CancelOrderRequest request, grpc::ServerCallContext context)
        {
            this.logger.LogInformation("Handling Order cancellation request");
            var result = new CancelOrderResponse();
            try
            {
                var agg = new OrderAggregate(request.OrderID);
                // TODO: load aggregate with order events
                var exists = this.orderManagementClient.OrderExists(new GetOrderRequest { OrderID = request.OrderID });
                if (!exists.Exists)
                {
                    result.Canceled = false;
                    return Task.FromResult(result);
                }
                var originalOrder = this.orderManagementClient.GetOrder(new GetOrderRequest { OrderID = request.OrderID });

                var productAggregates = this.GetProductAggregates(originalOrder.LineItems.ToArray());

                var evts = agg.Cancel(request.UserID, productAggregates);
                foreach (var evt in evts)
                {
                    this.eventEmitter.Emit(evt);
                }
                result.Canceled = true;
                result.ConfirmationCode = Guid.NewGuid().ToString(); // unused
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to cancel order");
                result.Canceled = false;
                return Task.FromResult(result);
            }
        }

        public override Task<OrderResponse> SubmitOrder(OrderRequest request, grpc::ServerCallContext context)
        {
            this.logger.LogInformation("Handling Order Request Submission");
            var response = new OrderResponse();

            if (!IsValidRequest(request))
            {
                response.Accepted = false;
                return Task.FromResult(response);
            }

            try
            {
                var agg = new OrderAggregate(Guid.NewGuid().ToString());
                var productAggregates = this.GetProductAggregates(request.LineItems.ToArray());
                var evts = agg.Accept(request, productAggregates);
                foreach (var evt in evts)
                {
                    this.eventEmitter.Emit(evt);
                }
                response.Accepted = true;
                response.OrderID = agg.OrderId;
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to submit order");
                response.Accepted = false;
                return Task.FromResult(response);
            }
        }

        private Dictionary<string, ProductAggregate> GetProductAggregates(IEnumerable<LineItem> lineItems)
        {
            var productAggregates = new Dictionary<string, ProductAggregate>();
            foreach (var lineItem in lineItems)
            {
                var activity = this.inventoryManagementClient.GetActivity(new GetProductRequest
                {
                    SKU = lineItem.SKU
                });
                var pagg = new ProductAggregate(lineItem.SKU);
                pagg.ApplyAll(activity.Activities.ToArray());
                productAggregates[lineItem.SKU] = pagg;
            }
            return productAggregates;
        }

        private static bool IsValidRequest(OrderRequest request)
        {
            if (request.LineItems.Count == 0)
            {
                return false;
            }
            if (request.TaxRate > 50)
            {
                return false;
            }

            return true;
        }
    }
}