using SharedKernel;
using MassTransit;
using MongoDB.Entities;
using SharedKernel.Events;
using BiddingService.Domain.Entities;
using BiddingService.Domain.Enums;

namespace BiddingService.Application.Consumers;
public class AuctionCreatedConsumer : IConsumer<AuctionCreatedEvent>
{
    public async Task Consume(ConsumeContext<AuctionCreatedEvent> context)
    {
        var auction = new Auction
        {
            ID = context.Message.Id.ToString(),
            Seller = context.Message.Seller,
            AuctionEnd = context.Message.AuctionEnd,
            ReservePrice = context.Message.ReservePrice,
            Status = AuctionStatus.Active
        };

        await auction.SaveAsync();
    }
}