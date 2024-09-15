using RoomService.Application.Events;

namespace SharedKernel.Events
{
    public class PaymentMadeEvent : IIntegrationEvent
    {
        public string EventName => "payment.made";
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public int? CouponId { get; set; }
        public CouponEvent? Coupon { get; set; }
        public string? CouponCode { get; set; }
        public double? Discount { get; set; }
        public double? Total { get; set; }
        public string? Name { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Status { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }
        public Guid? AuctionId { get; set; }
    }
}
