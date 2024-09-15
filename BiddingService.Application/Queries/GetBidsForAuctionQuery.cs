using BiddingService.Domain.Entities;
using MediatR;
using MongoDB.Entities;

namespace BiddingService.Application.Queries
{
    public class GetBidsForAuctionQuery : IRequest<List<BidResponse>>
    {
        public Guid AuctionId { get; init; }
    }

    public record BidResponse(
        string Id,
        string AuctionId,
        string Bidder,
        DateTime BidTime,
        int Amount,
        string BidStatus
    );

    public class GetBidsForAuctionQueryHandler : IRequestHandler<GetBidsForAuctionQuery, List<BidResponse>>
    {

        public GetBidsForAuctionQueryHandler()
        {
        }

        public async Task<List<BidResponse>> Handle(GetBidsForAuctionQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.AuctionId))
            {
                throw new ArgumentException("AuctionId cannot be null or empty", nameof(request.AuctionId));
            }

            var bids = await DB.Find<Bid>()
                .Match(a => a.AuctionId == request.AuctionId)
                .Sort(b => b.Descending(a => a.BidTime))
                .ExecuteAsync(cancellationToken);

            if (bids == null || bids.Count() == 0)
            {
                return new List<BidResponse>();
            }

            return bids.Adapt<List<BidResponse>>();
        }
    }
}
