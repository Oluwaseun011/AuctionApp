using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PaymentService;
using PaymentService.Application.Consumers;
using PaymentService.Application.Contracts;
using PaymentService.Infrastructure;
using PaymentService.Infrastructure.Repositories;
using PaymentService.Infrastructure.Services;

namespace PaymentService
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCustomIntegrationTransport(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<PaymentDbContext>(o =>
                {
                    o.QueryDelay = TimeSpan.FromSeconds(10);

                    o.UsePostgres();
                    o.UseBusOutbox();
                });

                x.AddConsumersFromNamespaceContaining<PaymentMadeFaultConsumer>();

                x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("payment", false));

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseRetry(r =>
                    {
                        r.Handle<RabbitMqConnectionException>();
                        r.Interval(5, TimeSpan.FromSeconds(10));
                    });
                    cfg.Host(configuration["RabbitMq:Host"], "/", host =>
                    {
                        host.Username(configuration.GetValue("RabbitMq:Username", "guest"));
                        host.Password(configuration.GetValue("RabbitMq:Password", "guest"));
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PaymentDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentGateway, PaystackServices>();

            return services;
        }
    }
}