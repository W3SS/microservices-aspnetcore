using System;
using System.Collections.Generic;
using System.Linq;

namespace PartialFoods.Services.InventoryServer.Entities
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryContext context;

        public InventoryRepository(InventoryContext context)
        {
            this.context = context;
        }

        public int GetCurrentQuantity(string sku)
        {
            var quantity = 0;
            try
            {
                var productActivities = this.GetActivity(sku);
                foreach (var activity in productActivities)
                {
                    if ((activity.ActivityType == ActivityType.Released) ||
                        (activity.ActivityType == ActivityType.StockAdd))
                    {
                        quantity += activity.Quantity;
                    }
                    else if (activity.ActivityType == ActivityType.Reserved)
                    {
                        quantity -= activity.Quantity;
                    }
                    // Shipped activity doesn't change quantity 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine($"Failed to query current quantity: {ex.ToString()}");
            }
            return quantity;
        }

        public Product GetProduct(string sku)
        {
            try
            {
                var product = this.context.Products.FirstOrDefault(p => p.SKU == sku);
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine($"Failed to query product - {sku}");
            }
            return null;
        }

        public IList<ProductActivity> GetActivity(string sku)
        {
            var activities = (from activity in this.context.Activities
                              where activity.SKU == sku
                              orderby activity.CreatedOn ascending
                              select activity).ToList();
            return activities;
        }

        public ProductActivity PutActivity(ProductActivity activity)
        {
            Console.WriteLine($"Attempting to put activity {activity.ActivityId}, type {activity.ActivityType.ToString()}");

            try
            {
                var existing = this.context.Activities.FirstOrDefault(a => a.ActivityId == activity.ActivityId);
                if (existing != null)
                {
                    Console.WriteLine($"Bypassing add for order activity {activity.ActivityId} - already exists.");
                    return activity;
                }

                this.context.Add(activity);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to store activity {ex.ToString()}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
            return activity;
        }
    }
}