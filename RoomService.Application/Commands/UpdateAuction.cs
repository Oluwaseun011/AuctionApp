using AutoMapper;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using RoomService.Application.Repositories;
using SharedKernel.Exceptions;

namespace RoomService.Application.Commands;

public class UpdateAuction
{
    public record UpdateAuctionCommand : IRequest<AuctionResponse>
    {
        public Guid Id { get; set; }
        public string Make { get; init; } = default!;
    }

    public class Handler : IRequestHandler<UpdateAuctionCommand, AuctionResponse>
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Handler> _logger;
        //private readonly ICurrentUser _currentUser;

        public Handler(IAuctionRepository auctionRepository, IUnitOfWork unitOfWork, ILogger<Handler> logger)
        {
            _auctionRepository = auctionRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            //_currentUser = currentUser;
        }

        public async Task<AuctionResponse> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken)
        {
            var existingAuction = await _auctionRepository.GetAuction(request.Id);
            if (existingAuction == null)
            {
                _logger.LogError("Auction with id {Id} does not exist", request.Id);
                throw new DomainException($"Auction with Id {request.Id} does not exist", ExceptionCodes.AuctionNotFound.ToString(), 404);
            }

            await _unitOfWork.SaveChangesAsync();

            return existingAuction.Adapt<AuctionResponse>();
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
