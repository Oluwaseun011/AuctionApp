using RoomService.Application.Events;

namespace SharedKernel.Events
{
    public class CouponEvent : IIntegrationEvent
    {
        public string EventName => "coupon";
        public int CouponId { get; set; }
        public string? CouponCode { get; set; }
        public double? DiscountAmount { get; set; }
        public int? MinAmount { get; set; }
    }
}
