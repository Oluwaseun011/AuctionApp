using RoomService.Application.Events;

namespace SharedKernel.Events
{
    public class AuctionDeletedEvent : IIntegrationEvent
    {
        public string EventName => "auction.deleted";
        public string Id { get; set; }
    }
}
