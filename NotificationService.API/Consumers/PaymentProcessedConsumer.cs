using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.API.Hubs;
using SharedKernel.Events;

namespace NotificationService.API.Consumers;

public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public PaymentProcessedConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        Console.WriteLine($"--> Payment processed: message received");
        await _hubContext.Clients.All.SendAsync("PaymentProcessed", context.Message);
    }
}
