using RoomService.Domain.Enums;
namespace RoomService.Domain.Entities
{
    public class Auction
    {
        public Auction( string code, string createdBy)
        {
            Code = code;
            CreatedBy = createdBy;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = default!;
        public Item Item { get; set; } 
        public AuctionStatus Status { get; set; }
        public DateTime AuctionStart { get; set; }
        public DateTime AuctionEnd { get; set; }
        
        public int ReservePrice { get; set; } = 0;
        public string Seller { get; set; } = default!;
        public string Winner { get; set; } = default!;
        public decimal? SoldAmount { get; set; }
        public int? CurrentHighBid { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public bool HasReservePrice() => ReservePrice > 0;

        public void UpdateDetails(string description)
        {
            if (Item != null)
            {
                Item.Description = description;
            }
        }
    }
}
