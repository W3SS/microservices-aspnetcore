using Microsoft.EntityFrameworkCore;

namespace PartialFoods.Services.OrderManagementServer.Entities
{
    public class OrdersContext : DbContext
    {
        private readonly string connStr;

        public OrdersContext(string connectionString)
        {
            this.connStr = connectionString;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<LineItem> OrderItems { get; set; }

        public DbSet<OrderActivity> Activities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(this.connStr);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasKey(o => o.OrderId);

            builder.Entity<LineItem>()
                .HasKey(li => new { li.OrderId, li.SKU });

            builder.Entity<OrderActivity>()
                .HasKey(a => new { a.OrderId, a.ActivityId });
        }
    }
}