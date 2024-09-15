using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var path = Path.Combine(Directory.GetCurrentDirectory(), "readme.md");
    var desc = $"Auction Service API";

    if (File.Exists(path))
        desc = File.ReadAllText(path);

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = $"Auction Service API",
        Version = "v1",
        Description = desc
    });
    c.ResolveConflictingActions(x => x.First());
});
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "username";
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("customPolicy", b =>
    {
        b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(settings =>
    {
        settings.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction API v1");
    });
}
app.UseCors("customPolicy");
app.MapReverseProxy();

app.UseAuthentication();
app.UseAuthorization();

app.Run();