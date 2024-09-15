using MassTransit;
using RoomService.Domain.Enums;
using SharedKernel.Events;

namespace RoomService.Application.Consumers
{
    public class AuctionFinishedConsumer : IConsumer<AuctionFinishedEvent>
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuctionFinishedConsumer(AuctionDbContext dbContext, IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<AuctionFinishedEvent> context)
        {
            Console.WriteLine("--> Consuming auction finished");

            var auction = await _dbContext.Auctions.FindAsync(Guid.Parse(context.Message.AuctionId));

            if (auction == null)
            {
                Console.WriteLine($"--> Auction with ID {context.Message.AuctionId} not found.");
                return;
            }

            if (context.Message.ItemSold)
            {
                auction.Winner = context.Message.Winner;
                auction.SoldAmount = context.Message.Amount;

                // Publish the AuctionWinnerNotified event to the Invoice Service
                var auctionWinnerNotified = new AuctionWinnerNotifiedEvent(
                    auctionId: context.Message.AuctionId,
                    itemDetails: new AuctionItemDetails(
                        itemId: auction.Item.Id.ToString(),// auction.ItemId,
                        name: auction.Item.Model,// auction.ItemName,
                        description: auction.Item.Make // auction.ItemDescription
                    ),
                    highestBidder: new BidderInfo(
                        bidderId: context.Message.HighestBidder.BidderId,
                        fullName: context.Message.HighestBidder.FullName,
                        email: context.Message.HighestBidder.Email
                    ),
                    winningBidAmount: context.Message.WinningBidAmount,
                    paymentTerms: new PaymentTerms(
                        dueDate: DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"),
                        currency: "USD"
                    ),
                    auctionCompletionDate: DateTime.UtcNow,
                    billingAddress: context.Message.BillingAddress,
                    invoiceDate: DateTime.UtcNow,
                    paymentInstructions: "Please pay the amount due within 7 days.",
                    refundPolicy: "Refunds are available within 30 days of payment."
                );

                await _publishEndpoint.Publish(auctionWinnerNotified);
            }

            auction.Status = auction.SoldAmount > auction.ReservePrice
                ? AuctionStatus.Completed
                : AuctionStatus.ReserveNotMet;

            await _dbContext.SaveChangesAsync();
        }
    }
}