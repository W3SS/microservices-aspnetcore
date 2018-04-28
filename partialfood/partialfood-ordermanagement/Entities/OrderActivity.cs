namespace PartialFoods.Services.OrderManagementServer.Entities
{
    public class OrderActivity
    {
        public string OrderId { get; set; }
        public ActivityType ActivityType { get; set; }
        public string UserId { get; set; }
        public long OccuredOn { get; set; }
        public string ActivityId { get; set; }
    }

    public enum ActivityType
    {
        Canceled = 1,
        Unknown = 0
    }
}