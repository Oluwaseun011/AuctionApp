using RoomService.Domain.Entities;
using SharedKernel.Paging;

namespace RoomService.Application.Repositories
{
    public interface IAuctionRepository
    {
        Task<bool> CheckCodeExistsAsync(string code);
        Task<Auction> CreateAuction(Auction auction);
        Task<Auction?> GetAuction(Guid id);
        Auction UpdateAuction(Auction auction);
        Task<IReadOnlyList<Auction>> GetAuctions();
        Task<PaginatedList<Auction>> GetAuctions(PageRequest pageRequest, bool usePaging = true);
    }
}
