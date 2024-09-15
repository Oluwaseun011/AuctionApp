using Microsoft.EntityFrameworkCore;
using RoomService.Domain.Entities;
using System.Reflection;

namespace RoomService.Infrastructure.Persistence.Context
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions opts) : base(opts)
        {
        }

        public DbSet<Auction> Auctions => Set<Auction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<string>().UseCollation("case_insensitive");
        }
    }
}
