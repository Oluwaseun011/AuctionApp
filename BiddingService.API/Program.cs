using BiddingService;
using BiddingService.API;
using BiddingService.Grpc.Services;
using BiddingService.Infrastructure;
using MongoDB.Driver;
using MongoDB.Entities;
using Polly;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer()
    .AddApiDoc()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
    .AddCustomAuthentication(builder.Configuration)
    .AddAutoMapper()
    .AddCustomIntegrationTransport(builder.Configuration)
    .AddControllers();

builder.Services.AddHostedService<CheckAuctionFinished>();
builder.Services.AddScoped<GrpcAuctionClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(settings =>
    {
        settings.SwaggerEndpoint("/swagger/v1/swagger.json", "Bidding API v1");
    });
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await Policy.Handle<TimeoutException>().WaitAndRetryAsync(5, t => TimeSpan.FromSeconds(10))
    .ExecuteAndCaptureAsync(async () =>
    {
        await DB.InitAsync("BidDb",
            MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("BidDbConnection")));
        await DbInitializer.InitDb(app);
    });


app.Run();