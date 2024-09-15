using RoomService.Application.Events;
namespace SharedKernel.Events
{
    public class AuctionUpdatedEvent : IIntegrationEvent
    {
        public string EventName => "auction.updated";
        public string Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
    }
}
