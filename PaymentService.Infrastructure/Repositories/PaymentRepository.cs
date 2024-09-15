using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities;

namespace PaymentService.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly PaymentDbContext _context;
    public PaymentRepository(PaymentDbContext context)
    {
        _context = context;
    }

    public void AddPayment(PaymentTransaction payment)
    {
        _context.PaymentTransactions.Add(payment);
    }

    public async Task<PaymentTransaction> GetPaymentByIdAsync(Guid id)
    {
        return await _context.PaymentTransactions.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void RemovePayment(PaymentTransaction payment)
    {
        _context.PaymentTransactions.Remove(payment);
    }

    public async Task<List<PaymentTransaction>> GetAllPaymentTransactions()
    {
        return await _context.PaymentTransactions.ToListAsync();
    }

    public void UpdatePayment(PaymentTransaction payment)
    {
        _context.PaymentTransactions.Update(payment);
    }
}