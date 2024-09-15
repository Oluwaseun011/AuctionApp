using System.Globalization;
using AuthService.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Sentry;
using Serilog;

namespace RoomService.Infrastructure.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigration(IServiceProvider serviceProvider, bool ensureDbCreated = false)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (ensureDbCreated)
            {
                EnsureDbCreation(context);
            }
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Error occurred while migrating");
            SentrySdk.CaptureException(ex);
            throw;
        }
    }

    private static void EnsureDbCreation(ApplicationDbContext context)
    {
        if (context.Database.CanConnect())
        {
            return;
        }
        var databaseName = context.Database.GetDbConnection().Database;
        var connectionString = context.Database.GetConnectionString()!.Replace($"Database={databaseName};", "", true, CultureInfo.InvariantCulture);

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        var cmd = connection.CreateCommand();
#pragma warning disable SCS0002
        cmd.CommandText = $"CREATE DATABASE \"{databaseName}\"";
        cmd.ExecuteScalar();
        connection.Close();
#pragma warning restore SCS0002
    }
}