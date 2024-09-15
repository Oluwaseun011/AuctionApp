using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using RoomService.Application.Repositories;
using RoomService.Domain.Entities;
using SharedKernel.Exceptions;

namespace RoomService.Application.Commands;
public class CreateAuction
{
    public record CreateAuctionCommand(string Code) : IRequest<AuctionResponse>;
    

    public class Handler : IRequestHandler<CreateAuctionCommand, AuctionResponse>
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

        public async Task<AuctionResponse> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
        {
            var codeExists = await _auctionRepository.CheckCodeExistsAsync(request.Code);
            if (codeExists)
            {
                _logger.LogError("Code with code {Code} already exists", request.Code);
                throw new DomainException($"Code with code {request.Code} already exists", ExceptionCodes.AuctionAlreadyExists.ToString(), 400);
            }

            //var user = await _currentUser.GetUserAsync();
            var auction = new Auction(request.Code, "user.Email");
            await _auctionRepository.CreateAuction(auction);
            await _unitOfWork.SaveChangesAsync();

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

    public class CommandValidator : AbstractValidator<CreateAuctionCommand>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");
        }
    }
}


