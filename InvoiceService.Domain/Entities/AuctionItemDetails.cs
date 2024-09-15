using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceService.Domain.Entities
{
    public class AuctionItemDetails
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
