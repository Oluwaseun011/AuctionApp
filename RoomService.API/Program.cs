using Npgsql;
using Polly;
using RoomService;
using RoomService.Infrastructure;
using RoomService.Infrastructure.Extensions;
using RoomService.Infrastructure.Grpc.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer()
       .AddApiDoc()
       .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
       .AddCustomDbContext(builder.Configuration)
       .AddCustomAuthentication(builder.Configuration)
       .AddCustomServices()
       .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
       .AddCustomIntegrationTransport(builder.Configuration)
       .AddControllers();

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(settings =>
    {
        settings.SwaggerEndpoint("/swagger/v1/swagger.json", "Room API v1");
    });
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<GrpcAuctionService>();

// Use this to retry if container is not ready
var retryPolicy = Policy.Handle<NpgsqlException>().WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(10));
retryPolicy.ExecuteAndCapture(() => DbInitializer.InitDb(app));


app.Run();

// We need this class to reference in our integration tests
public partial class Program
{
}