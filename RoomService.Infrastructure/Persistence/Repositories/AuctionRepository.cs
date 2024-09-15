using Microsoft.EntityFrameworkCore;
using RoomService.Application.Repositories;
using RoomService.Domain.Entities;
using RoomService.Infrastructure.Persistence.Context;
using SharedKernel.Paging;

namespace RoomService.Infrastructure.Persistence.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AuctionDbContext _context;
        public AuctionRepository(AuctionDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckCodeExistsAsync(string code)
        {
            return await _context.Auctions.AnyAsync(a => a.Code == code);
        }

        public async Task<Auction> CreateAuction(Auction auction)
        {
            await _context.AddAsync(auction);
            return auction;
        }

        public async Task<Auction?> GetAuction(Guid id)
        {
            return await _context.Auctions.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IReadOnlyList<Auction>> GetAuctions()
        {
            return await _context.Auctions.ToListAsync();
        }

        public async Task<PaginatedList<Auction>> GetAuctions(PageRequest pageRequest, bool usePaging = true)
        {
            var query = _context.Auctions.AsQueryable();

            if (!string.IsNullOrEmpty(pageRequest?.Keyword))
            {
                var helper = new EntitySearchHelper<Auction>(_context);
                query = helper.SearchEntity(pageRequest.Keyword);
            }

            query = query.OrderBy(r => r.Code);

            var totalItemsCount = await query.CountAsync();
            if (usePaging)
            {
                var offset = (pageRequest.Page - 1) * pageRequest.PageSize;
                var result = await query.Skip(offset).Take(pageRequest.PageSize).ToListAsync();
                return result.ToPaginatedList(totalItemsCount, pageRequest.Page, pageRequest.PageSize);
            }
            else
            {
                var result = await query.ToListAsync();
                return result.ToPaginatedList(totalItemsCount, 1, totalItemsCount);
            }
        }

        public Auction UpdateAuction(Auction auction)
        {
            return _context.Auctions.Update(auction).Entity;
        }
       
    }
}
