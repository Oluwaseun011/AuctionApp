using RoomService.Application.Events;

namespace SharedKernel.Events;
public class InvoiceGeneratedEvent : IIntegrationEvent
{
    public string EventName => "invoice.generated";
    public Guid InvoiceId { get; set; }
    public string AuctionId { get; set; }
    public AuctionItemDetails ItemDetails { get; set; }
    public BidderInfo HighestBidder { get; set; }
    public double WinningBidAmount { get; set; }
    public PaymentTerms PaymentTerms { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string BillingAddress { get; set; }
    public string PaymentInstructions { get; set; }
    public string RefundPolicy { get; set; }
    public decimal TaxesAndFees { get; set; }
}