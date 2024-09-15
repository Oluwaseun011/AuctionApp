using SharedKernel;
using MassTransit;
using SharedKernel.Events;

namespace PaymentService.Application.Consumers;
public class PaymentMadeFaultConsumer : IConsumer<Fault<PaymentMadeEvent>>
{
    public async Task Consume(ConsumeContext<Fault<PaymentMadeEvent>> context)
    {
        var exceptionInfo = context.Message.Exceptions.FirstOrDefault();
        if (exceptionInfo != null)
        {
            Console.WriteLine($"Exception Type: {exceptionInfo.ExceptionType}");
            Console.WriteLine($"Exception Message: {exceptionInfo.Message}");
        }

        await context.Publish<IPaymentFaulted>(new
        {
            PaymentId = context.Message.Message.Id,
            Reason = "Failed to process payment.",
            ExceptionMessage = exceptionInfo?.Message,
            Timestamp = DateTime.UtcNow
        });

    }
}

public interface IPaymentFaulted
{
    Guid PaymentId { get; }
    string Reason { get; }
    string ExceptionMessage { get; }
    DateTime Timestamp { get; }
}