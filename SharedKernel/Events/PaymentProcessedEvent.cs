using RoomService.Application.Events;

namespace SharedKernel.Events;

public class PaymentProcessedEvent : IIntegrationEvent
{
    public string EventName => "payment.processed";
    public Guid PaymentId { get; set; }
    public string AuctionId { get; set; }
    public Guid InvoiceId { get; set; }
    public double AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Status { get; set; }
}
