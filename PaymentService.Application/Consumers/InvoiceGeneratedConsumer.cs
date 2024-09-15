using MassTransit;
using PaymentService.Application.Contracts;
using PaymentService.Domain.Entities;
using SharedKernel.Events;

namespace PaymentService.Application.Consumers
{
    public class InvoiceGeneratedConsumer : IConsumer<InvoiceGeneratedEvent>
    {
        private readonly PaymentDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IPaymentGateway _paymentGateway;

        public InvoiceGeneratedConsumer(PaymentDbContext dbContext, IPublishEndpoint publishEndpoint, IPaymentGateway paymentGateway)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
            _paymentGateway = paymentGateway;
        }

        
        public async Task Consume(ConsumeContext<InvoiceGeneratedEvent> context)
        {
            Console.WriteLine("--> Consuming InvoiceGenerated event");

            var invoiceGenerated = context.Message;

            // Process the payment using a payment gateway
            var paymentResult = await _paymentGateway.MakePayment(invoiceGenerated.WinningBidAmount, invoiceGenerated.InvoiceId.ToString(), invoiceGenerated.HighestBidder.Email);

            // Determine payment status
            var paymentStatus = paymentResult.Status ? "Success" : "Failed";
            if (paymentStatus == "Failed")
            {
                Console.WriteLine($"--> Payment failed for Invoice ID: {invoiceGenerated.InvoiceId}. Reason: {paymentResult.Message}");
                // Optionally, log or notify about the failure
            }

            // Create a new payment transaction record
            var paymentTransaction = new PaymentTransaction
            {
                Id = Guid.NewGuid(),
                AuctionId = Guid.Parse(invoiceGenerated.AuctionId),
                InvoiceId = invoiceGenerated.InvoiceId,
                AmountPaid = invoiceGenerated.WinningBidAmount,
                PaymentDate = DateTime.UtcNow,
                Status = paymentStatus // Set to "Success" or "Failed" based on paymentResult
            };

            // Save the payment transaction to the database
            await _dbContext.PaymentTransactions.AddAsync(paymentTransaction);
            await _dbContext.SaveChangesAsync();

            Console.WriteLine($"--> Payment processed for Invoice ID: {invoiceGenerated.InvoiceId}, Payment ID: {paymentTransaction.Id}, Status: {paymentTransaction.Status}");

            // Create the PaymentProcessed event
            var paymentProcessedEvent = new PaymentProcessedEvent
            {
                PaymentId = paymentTransaction.Id,
                AuctionId = paymentTransaction.AuctionId.ToString(),
                InvoiceId = paymentTransaction.InvoiceId,
                AmountPaid = paymentTransaction.AmountPaid,
                PaymentDate = paymentTransaction.PaymentDate,
                Status = paymentTransaction.Status
            };

            // Publish the PaymentProcessed event
            await _publishEndpoint.Publish(paymentProcessedEvent);

            Console.WriteLine($"--> PaymentProcessed event published for Payment ID: {paymentTransaction.Id}, Status: {paymentTransaction.Status}");
        }
    }
}
