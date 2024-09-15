using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.API.Hubs;
using SharedKernel.Events;

namespace NotificationService.API.Consumers
{
    public class AuctionWinnerNotifiedConsumer : IConsumer<AuctionWinnerNotifiedEvent>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public AuctionWinnerNotifiedConsumer(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<AuctionWinnerNotifiedEvent> context)
        {
            Console.WriteLine($"--> Auction winner notified: message received");
            await _hubContext.Clients.All.SendAsync("AuctionWinnerNotified", context.Message);
        }
    }
}
