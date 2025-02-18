namespace EcomCore.Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? ShippingAddress { get; set; }
        public string? TotalAmount { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
