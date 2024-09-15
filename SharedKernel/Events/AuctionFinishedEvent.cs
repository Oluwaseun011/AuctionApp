using RoomService.Application.Events;

namespace SharedKernel.Events
{
    public class AuctionFinishedEvent : IIntegrationEvent
    {
        public string EventName => "auction.finished";
        public string AuctionId { get; set; }
        public AuctionItemDetails ItemDetails { get; set; }
        public BidderInfo HighestBidder { get; set; }
        public decimal? WinningBidAmount { get; set; }
        public PaymentTerms PaymentTerms { get; set; }
        public DateTime AuctionCompletionDate { get; set; }
        public string BillingAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string PaymentInstructions { get; set; }
        public string RefundPolicy { get; set; }
        public bool ItemSold { get; set; }
        public string Winner { get; set; }
        public string Seller { get; set; }
        public decimal? Amount { get; set; }
    }
}
