using Mapster;
using MediatR;
using RoomService.Application.Repositories;
using SharedKernel.Exceptions;

namespace RoomService.Application.Queries;

public class GetAuction
{

    public record Query : IRequest<AuctionResponse>
    {
        public Guid Id { get; set; } = default!;
    }

    public class Handler : IRequestHandler<Query, AuctionResponse>
    {
        private readonly IAuctionRepository _auctionRepository;

        public Handler(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<AuctionResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var auction = await _auctionRepository.GetAuction(request.Id) ?? throw new DomainException($"Auction with id {request.Id} does not exist", ExceptionCodes.AuctionNotFound.ToString(), 404);

            return auction.Adapt<AuctionResponse>();
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
