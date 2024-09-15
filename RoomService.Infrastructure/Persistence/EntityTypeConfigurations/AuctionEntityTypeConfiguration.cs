using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomService.Domain.Entities;

namespace RoomService.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class AuctionEntityTypeConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.ToTable("auctions");

            builder.HasKey(b => b.Code);

            builder.Property(b => b.Code)
                .HasColumnType("varchar(50)")
                .HasColumnName("code")
                .IsRequired(); ;

            builder.HasIndex(p => p.Code).IsUnique();

            builder.Property(b => b.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(b => b.ModifiedDate)
                .HasColumnName("modified_date");

            builder.Property(op => op.CreatedBy)
               .HasColumnName("created_by")
               .HasColumnType("varchar(255)");

            builder.Property(op => op.ModifiedBy)
                .HasColumnName("modified_by")
                .HasColumnType("varchar(255)");

            builder.HasIndex(p => p.Code).IsUnique();

        }

    }
}
