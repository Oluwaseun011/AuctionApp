using InvoiceService.Domain.Entities;
using InvoiceService.Infrastructure;
using MassTransit;
using SharedKernel.Events;

namespace InvoiceService.Application.Consumers
{
    public class AuctionWinnerNotifiedConsumer : IConsumer<AuctionWinnerNotifiedEvent>
    {
        private readonly InvoiceDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        public AuctionWinnerNotifiedConsumer(InvoiceDbContext dbContext,IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<AuctionWinnerNotifiedEvent> context)
        {
            Console.WriteLine("--> Consuming AuctionWinnerNotified event");

            var auctionWinnerNotified = context.Message;

            // Map AuctionWinnerNotified to Invoice
            var invoice = _mapper.Map<Invoice>(auctionWinnerNotified);

            // Create a new invoice
            invoice.InvoiceId = Guid.NewGuid(); // Set a new unique ID for the invoice

            // Save the invoice to the database
            await _dbContext.Invoices.AddAsync(invoice);
            await _dbContext.SaveChangesAsync();

            // Create the InvoiceGenerated event
            var invoiceGeneratedEvent = _mapper.Map<InvoiceGeneratedEvent>(invoice);

            // Publish the InvoiceGenerated event
            await _publishEndpoint.Publish(invoiceGeneratedEvent);

            Console.WriteLine($"--> InvoiceGenerated event published for Invoice ID: {invoice.InvoiceId}");
        }
    }
}
