using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PartialFoods.Services.OrderManagementServer.Entities
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersContext context;

        public OrderRepository(OrdersContext context)
        {
            this.context = context;
        }

        public OrderActivity AddActivity(OrderActivity activity)
        {
            try
            {
                this.context.Activities.Add(activity);
                this.context.SaveChanges();
                return activity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine($"Failed to add order activity: {ex}");
                return null;
            }
        }

        public bool OrderExists(string orderId)
        {
            try
            {
                var existing = this.context.Orders.FirstOrDefault(o => o.OrderId == orderId);
                return existing != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine($"Failed to check order existence: {ex}");
                return false;
            }
        }
        public Order GetOrder(string orderId)
        {
            Console.WriteLine($"Fetching order {orderId}");
            try
            {
                var existing = this.context.Orders
                    .Include(o => o.LineItems)
                    .Include(o => o.Activities)
                    .FirstOrDefault(o => o.OrderId == orderId);
                return existing;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine($"Failed to query order {ex}");
                return null;
            }
        }

        public Order Add(Order order)
        {
            Console.WriteLine($"Adding order {order.OrderId} to repository.");
            try
            {
                var existing = this.context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
                if (existing != null)
                {
                    Console.WriteLine($"Bypassing add for order {order.OrderId} - already exists.");
                    return order;
                }

                this.context.Add(order);
                this.context.SaveChanges();
                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine($"Failed to save changes in db context: {ex}");
                return null;
            }
        }
    }
}