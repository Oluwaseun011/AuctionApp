using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceService.Domain.Entities
{
    public class PaymentTerms
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DueDate { get; set; }
        public string Currency { get; set; }
    }
}
