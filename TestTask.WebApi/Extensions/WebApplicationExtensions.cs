using FluentMigrator.Runner;
using TestTask.DAL.Interfaces;

namespace TestTask.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var migrator = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        if (migrator.HasMigrationsToApplyUp())
        {
            migrator.MigrateUp();
        }
    }
    public static void SeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();

        seeder.Apply();
    }
}


