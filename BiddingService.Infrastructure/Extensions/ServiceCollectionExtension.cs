using BiddingService.Application.Consumers;
using BiddingService.Infrastructure.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace BiddingService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApiDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "readme.md");
                var desc = $"Bidding Service API";

                if (File.Exists(path))
                    desc = File.ReadAllText(path);

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Bidding Service API",
                    Version = "v1",
                    Description = desc
                });

                // Add JWT Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });

                c.ResolveConflictingActions(x => x.First());
            });

            return services;
        }



        public static IServiceCollection AddCustomIntegrationTransport(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

                x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("bids", false));

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

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>

              {
                  options.Authority = configuration["IdentityServiceUrl"];
                  options.RequireHttpsMetadata = false;
                  options.TokenValidationParameters.ValidateAudience = false;
                  options.TokenValidationParameters.NameClaimType = "username";
              });

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
         => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}