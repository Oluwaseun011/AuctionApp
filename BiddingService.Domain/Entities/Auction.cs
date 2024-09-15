using BiddingService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiddingService.Domain.Entities
{
    public class Auction
    {
        public string Seller { get; set; } = default!;
        public int ReservePrice { get; set; }
        public DateTime AuctionEnd { get; set; }
        public AuctionStatus Status { get; set; }
    }
}
