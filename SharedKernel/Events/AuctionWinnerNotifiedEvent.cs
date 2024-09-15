using RoomService.Application.Events;

namespace SharedKernel.Events
{
    public class AuctionWinnerNotifiedEvent : IIntegrationEvent
    {
        public string EventName => "auction.winner";
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

        public AuctionWinnerNotifiedEvent(string auctionId, AuctionItemDetails itemDetails, BidderInfo highestBidder,
            decimal? winningBidAmount, PaymentTerms paymentTerms, DateTime auctionCompletionDate,
            string billingAddress, DateTime invoiceDate, string paymentInstructions, string refundPolicy)
        {
            AuctionId = auctionId;
            ItemDetails = itemDetails;
            HighestBidder = highestBidder;
            WinningBidAmount = winningBidAmount;
            PaymentTerms = paymentTerms;
            AuctionCompletionDate = auctionCompletionDate;
            BillingAddress = billingAddress;
            InvoiceDate = invoiceDate;
            PaymentInstructions = paymentInstructions;
            RefundPolicy = refundPolicy;
        }
    }
    public class AuctionItemDetails
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public AuctionItemDetails(string itemId, string name, string description)
        {
            ItemId = itemId;
            Name = name;
            Description = description;
        }
    }

    public class BidderInfo
    {
        public string BidderId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public BidderInfo(string bidderId, string fullName, string email)
        {
            BidderId = bidderId;
            FullName = fullName;
            Email = email;
        }
    }
    public class PaymentTerms
    {
        public string DueDate { get; set; }
        public string Currency { get; set; }

        public PaymentTerms(string dueDate, string currency)
        {
            DueDate = dueDate;
            Currency = currency;
        }
    }
}
