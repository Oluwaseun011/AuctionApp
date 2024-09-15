using Mapster;
using MediatR;
using RoomService.Application.Repositories;
using SharedKernel.Paging;

namespace RoomService.Application.Queries;

public class GetAuctions
{
    public record Query : PageRequest, IRequest<PaginatedList<AuctionResponse>>
    {
        public bool UsePaging { get; init; } = true;
    }

    public class Handler : IRequestHandler<Query, PaginatedList<AuctionResponse>>
    {
        private readonly IAuctionRepository _auctionRepository;

        public Handler(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<PaginatedList<AuctionResponse>> Handle(Query request, CancellationToken cancellationToken)
        {

            var auctions = await _auctionRepository.GetAuctions();

            if (auctions == null || !auctions.Any())
            {
                return new PaginatedList<AuctionResponse>();
            }

            return auctions.Adapt<PaginatedList<AuctionResponse>>();
        }
    }

    public record AuctionResponse(
    Guid Id,
    int ReservePrice,
    string Seller,
    string Winner,
    int SoldAmount,
    int CurrentHighBid,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime AuctionEnd,
    string Status,
    string Name,
    string Description,
    string ItemImageUrl);

}
