using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.API.Hubs;
using SharedKernel.Events;

namespace NotificationService.API.Consumers;


public class AuctionCreatedConsumer : IConsumer<AuctionCreatedEvent>
{
    private readonly IHubContext<NotificationHub> _hubContext;

   
    public AuctionCreatedConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

   
    public async Task Consume(ConsumeContext<AuctionCreatedEvent> context)
    {
        Console.WriteLine($"--> Auction created: message received");
        await _hubContext.Clients.All.SendAsync("AuctionCreated", context.Message);
    }
}