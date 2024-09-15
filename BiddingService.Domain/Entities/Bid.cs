using BiddingService.Domain.Enums;

namespace BiddingService.Domain.Entities
{
    public class Bid
    {
        public Guid AuctionId { get; set; }
        public string Bidder { get; set; } = default!;
        public DateTime BidTime { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        public BidStatus BidStatus { get; set; }
    }
}
