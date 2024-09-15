namespace RoomService.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AuctionId { get; set; }
        public Auction? Auction { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        
    }
}
