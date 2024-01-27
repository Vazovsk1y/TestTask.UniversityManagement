using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestTask.DAL.Interfaces;

namespace TestTask.DAL.Extensions;

public static class Registrator
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbConnectionFactory, NpgsqlDbConnectionFactory>();
        services.AddScoped<IDatabaseSeeder, PostgresDatabaseSeeder>();

        string connectionString = configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string is not found.");

        services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                {
                    config
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.All();
                })
                .AddLogging(e => e.AddFluentMigratorConsole());

        return services;
    }
}
