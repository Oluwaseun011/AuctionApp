namespace PaymentService.Domain.Entities;

public class PaymentTransaction
{
    public Guid Id { get; set; }
    public Guid AuctionId { get; set; }
    public Guid InvoiceId { get; set; }
    public double AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Status { get; set; }
}
