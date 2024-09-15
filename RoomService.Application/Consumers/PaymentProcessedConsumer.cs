using MassTransit;
using RoomService.Domain.Enums;
using RoomService.Infrastructure.Persistence.Context;
using SharedKernel.Events;

namespace RoomService.Application.Consumers
{
    public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent>
    {
        private readonly AuctionDbContext _dbContext;

        public PaymentProcessedConsumer(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
        {
            Console.WriteLine("--> Consuming PaymentProcessed event");

            var paymentProcessed = context.Message;

            var auction = await _dbContext.Auctions.FindAsync(Guid.Parse(paymentProcessed.AuctionId));

            if (auction == null)
            {
                Console.WriteLine($"--> Auction with ID {paymentProcessed.AuctionId} not found.");
                return;
            }
            auction.Status = paymentProcessed.Status switch
            {
                "Success" => AuctionStatus.Paid,
                "Failed" => AuctionStatus.Failed,
                "Disputed" => AuctionStatus.Disputed,
                _ => AuctionStatus.PaymentPending,
            };

            _dbContext.Auctions.Update(auction);
            await _dbContext.SaveChangesAsync();

            Console.WriteLine($"--> Auction with ID {auction.Id} updated to status {auction.Status}");
        }
    }
}
