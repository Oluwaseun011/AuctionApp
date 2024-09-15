using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceService.Domain.Entities
{
    public class BidderInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string BidderId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
