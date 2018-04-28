using Microsoft.AspNetCore.Mvc;
using PartialFoods.Services.APIServer.Models;
using System;
using Microsoft.Extensions.Logging;

namespace PartialFoods.Services.APIServer.Controllers
{
    using System.Linq;

    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly OrderManagement.OrderManagementClient orderManagementClient;
        private readonly OrderCommand.OrderCommandClient orderCommandClient;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(OrderManagement.OrderManagementClient client,
        OrderCommand.OrderCommandClient orderCommandClient,
        ILogger<OrdersController> logger)
        {
            this.orderManagementClient = client;
            this.orderCommandClient = orderCommandClient;
            this.logger = logger;

            logger.LogInformation("Created Orders Controller");
        }

        [HttpPost]
        public NewOrderResponse CreateOrder([FromBody]NewOrderRequest newOrder)
        {
            logger.LogInformation("Accepting new order for user {0}", newOrder.UserId);
            var req = new OrderRequest
            {
                CreatedOn = (ulong)DateTime.UtcNow.Ticks,
                UserID = newOrder.UserId,
                TaxRate = (uint)newOrder.TaxRate,
                ShippingInfo = new ShippingInfo(), // ignoring this detail for now

            };
            foreach (var li in newOrder.LineItems)
            {
                req.LineItems.Add(new LineItem
                {
                    SKU = li.SKU,
                    Quantity = (uint)li.Quantity
                });
            }
            var result = this.orderCommandClient.SubmitOrder(req);
            return new NewOrderResponse
            {
                Accepted = result.Accepted,
                OrderId = result.OrderID
            };
        }

        [HttpDelete("{orderid}")]
        public CancelOrderResponse CancelOrder(string orderid)
        {
            // TODO : don't hardcode the user ID. Ideally this would come from a propogated bearer token
            // exposed via middleware
            var result = this.orderCommandClient.CancelOrder(new CancelOrderRequest { OrderID = orderid, UserID = "kevin" });
            return new CancelOrderResponse
            {
                OrderID = result.OrderID,
                Canceled = result.Canceled,
                ConfirmationCode = result.ConfirmationCode
            };
        }

        [HttpGet("{orderid}")]
        public OrderDetails GetOrder(string orderid)
        {
            var internalOrder = this.orderManagementClient.GetOrder(new GetOrderRequest { OrderID = orderid });

            OrderDetails response = new OrderDetails
            {
                OrderId = internalOrder.OrderID,
                CreatedOn = internalOrder.CreatedOn,
                TaxRate = internalOrder.TaxRate,
                Status = "open",
                LineItems = (
                    from li in internalOrder.LineItems
                    select new OrderItem
                    {
                        SKU = li.SKU,
                        Quantity = li.Quantity,
                        UnitPrice = li.UnitPrice
                    }
                ).ToArray()
            };

            if (internalOrder.Status == OrderStatus.Canceled)
            {
                response.Status = "canceled";
            }

            return response;
        }
    }
}