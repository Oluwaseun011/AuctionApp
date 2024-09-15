using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.API.Hubs;
using SharedKernel.Events;

namespace NotificationService.API.Consumers;

public class InvoiceGeneratedConsumer : IConsumer<InvoiceGeneratedEvent>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public InvoiceGeneratedConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<InvoiceGeneratedEvent> context)
    {
        Console.WriteLine($"--> Invoice generated: message received");
        await _hubContext.Clients.All.SendAsync("InvoiceGenerated", context.Message);
    }
}
