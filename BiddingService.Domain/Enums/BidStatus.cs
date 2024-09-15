using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiddingService.Domain.Enums
{
    public enum BidStatus
    {
        Accepted,
        AcceptedBelowReserve,
        TooLow,
        Finished
    }
}
