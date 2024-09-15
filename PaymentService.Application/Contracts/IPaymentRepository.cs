using PaymentService.Domain.Entities;

namespace PaymentService.Application.Contracts;
public interface IPaymentRepository
{
    void AddPayment(PaymentTransaction payment);
    Task<PaymentTransaction> GetPaymentByIdAsync(Guid id);
    Task<bool> SaveChangesAsync();
    void RemovePayment(PaymentTransaction payment);
    void UpdatePayment(PaymentTransaction payment);
}