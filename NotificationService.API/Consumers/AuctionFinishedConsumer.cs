using SharedKernel;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.API.Hubs;
using SharedKernel.Events;

namespace NotificationService.API.Consumers;
public class AuctionFinishedConsumer : IConsumer<AuctionFinishedEvent>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public AuctionFinishedConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<AuctionFinishedEvent> context)
    {
        Console.WriteLine($"--> Auction finished: message received");
        await _hubContext.Clients.All.SendAsync("AuctionFinished", context.Message);
    }
}