using Npgsql;
using PaymentService;
using Polly;
using Serilog;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
// Add services to the container.
builder.Services
       .AddCustomDbContext(builder.Configuration)
       .AddHttpClient()
       .AddCustomServices()
       .AddCustomIntegrationTransport(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext());

var app = builder.Build();

app.UseSerilogRequestLogging();

var retryPolicy = Policy.Handle<NpgsqlException>().WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(10));

app.Run();