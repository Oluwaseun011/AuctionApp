using MassTransit;
using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities;

namespace PaymentService.Infrastructure;
public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
    {
    }

    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configures the entity model for the inbox feature of MassTransit, used to ensure reliable message consumption.
        modelBuilder.AddInboxStateEntity();

        // Configures the entity model for the outbox message feature of MassTransit, enabling reliable messaging by storing messages before they are published.
        modelBuilder.AddOutboxMessageEntity();

        // Configures the entity model for the outbox state feature of MassTransit, used to track the state of messages in the outbox.
        modelBuilder.AddOutboxStateEntity();
    }
}