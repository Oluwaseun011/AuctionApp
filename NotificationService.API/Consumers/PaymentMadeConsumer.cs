using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.API.Hubs;
using SharedKernel.Events;

namespace NotificationService.API.Consumers;
public class PaymentMadeConsumer : IConsumer<PaymentMadeEvent>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public PaymentMadeConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<PaymentMadeEvent> context)
    {
        Console.WriteLine("--> payment made message received");
        await _hubContext.Clients.All.SendAsync("PaymentMade", context.Message);
    }
}