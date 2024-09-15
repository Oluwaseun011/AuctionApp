using SharedKernel;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.API.Hubs;
using SharedKernel.Events;

namespace NotificationService.API.Consumers;

public class BidPlacedConsumer : IConsumer<BidPlacedEvent>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public BidPlacedConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<BidPlacedEvent> context)
    {
        Console.WriteLine("--> bid placed message received");

        await _hubContext.Clients.All.SendAsync("BidPlaced", context.Message);
    }
}