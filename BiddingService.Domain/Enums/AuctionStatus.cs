using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiddingService.Domain.Enums
{
    public enum AuctionStatus
    {
        Pending,
        Active,
        Finished,
        ReserveNotMet,
        PaymentPending,
        Paid,
        Failed,
        Disputed
    }
}
