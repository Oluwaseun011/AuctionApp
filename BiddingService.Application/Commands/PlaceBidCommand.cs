using BiddingService.Domain.Entities;
using BiddingService.Domain.Enums;
using Mapster;
using MassTransit;
using MediatR;
using MongoDB.Entities;
using SharedKernel.Events;
using SharedKernel.Exceptions;

namespace BiddingService.Application.Commands
{
    public class PlaceBidCommand : IRequest<BidResponse>
    {
        public string AuctionId { get; init; }
        public int Amount { get; init; }
        public string Bidder { get; init; }
    }
    public record BidResponse(
       string Id,
       string AuctionId,
       string Bidder,
       DateTime BidTime,
       int Amount,
       string BidStatus);

    public class PlaceBidCommandHandler : IRequestHandler<PlaceBidCommand, BidResponse>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PlaceBidCommandHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task<BidResponse> Handle(PlaceBidCommand request, CancellationToken cancellationToken)
        {
            ValidateRequest(request);

            var auction = await DB.Find<Auction>()
                                  .OneAsync(request.AuctionId, cancellationToken);

            if (auction == null)
                throw new DomainException($"Auction  does not exist", ExceptionCodes.AuctionNotFound.ToString(), 404);

            ValidateAuctionState(auction, request);

            var bid = CreateBid(request, auction);
            SetBidStatus(bid, auction, cancellationToken);

            await SaveBidAsync(bid);
            await PublishBidPlacedEventAsync(bid, cancellationToken);

            return bid.Adapt<BidResponse>();
        }

        #region Private Methods
        private void ValidateRequest(PlaceBidCommand request)
        {
            if (string.IsNullOrEmpty(request.AuctionId))
                throw new ArgumentException("AuctionId cannot be null or empty", nameof(request.AuctionId));

            if (string.IsNullOrEmpty(request.Bidder))
                throw new ArgumentException("Bidder cannot be null or empty", nameof(request.Bidder));

            if (request.Amount <= 0)
                throw new ArgumentException("Bid amount must be greater than zero", nameof(request.Amount));
        }

        private void ValidateAuctionState(Auction auction, PlaceBidCommand request)
        {
            if (auction.Seller == request.Bidder)
                throw new DomainException($"Auction with id  does not exist", ExceptionCodes.AuctionNotFound.ToString(), 404);

            if (auction.AuctionEnd < DateTime.UtcNow)
                throw new DomainException($"Auction has closed", ExceptionCodes.Auctionclosed.ToString(), 404);
        }

        private Bid CreateBid(PlaceBidCommand request, Auction auction)
        {
            return new Bid
            {
                Amount = request.Amount,
                //AuctionId = request.AuctionId,
                Bidder = request.Bidder,
                BidStatus = auction.AuctionEnd < DateTime.UtcNow ? BidStatus.Finished : BidStatus.TooLow,
                BidTime = DateTime.UtcNow
            };
        }

        private async void SetBidStatus(Bid bid, Auction auction, CancellationToken cancellationToken)
        {
            var highBid = await DB.Find<Bid>()
                 .Match(a => a.AuctionId == bid.AuctionId)
                 .Sort(b => b.Descending(x => x.Amount))
                 .ExecuteFirstAsync();

            if (highBid == null || bid.Amount > highBid.Amount)
            {
                bid.BidStatus = bid.Amount > auction.ReservePrice
                    ? BidStatus.Accepted
                    : BidStatus.AcceptedBelowReserve;
            }
            else
            {
                bid.BidStatus = BidStatus.TooLow;
            }
        }

        private async Task SaveBidAsync(Bid bid)
        {
            await DB.SaveAsync(bid);
        }

        private async Task PublishBidPlacedEventAsync(Bid bid, CancellationToken cancellationToken)
        {
            var bidPlacedEvent = bid.Adapt<BidPlacedEvent>();
            await _publishEndpoint.Publish(bidPlacedEvent, cancellationToken);
        } 
        #endregion
    }
}
