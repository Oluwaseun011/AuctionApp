using InvoiceService.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Infrastructure;
public class InvoiceDbContext : DbContext
{
    public InvoiceDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Invoice> Invoices { get; set; }

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