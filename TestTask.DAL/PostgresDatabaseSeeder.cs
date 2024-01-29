using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Diagnostics;
using TestTask.DAL.Constants;
using TestTask.DAL.Interfaces;
using Dapper.Transaction;
using TestTask.DAL.Extensions;

namespace TestTask.DAL;

internal class PostgresDatabaseSeeder(
    IDbConnectionFactory connectionFactory, 
    ILogger<PostgresDatabaseSeeder> logger) : IDatabaseSeeder
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;
    private readonly ILogger<PostgresDatabaseSeeder> _logger = logger;

    public void Apply()
    {
        using var connection = _connectionFactory.Create();
        connection.Open();

        if (IsAbleToApply(connection))
        {
            Seed(connection);
        }
    }

    private void Seed(IDbConnection connection)
    {
        _logger.LogInformation("Data seeding started.");
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        using var transaction = connection.BeginTransaction();
        _logger.LogInformation("Transaction began.");

        bool transactionCommited = true;
        try
        {
            transaction.Insert(Tables.Departaments, Data.Departaments.Value);
            transaction.Insert(Tables.Groups, Data.Groups.Value);
            transaction.Insert(Tables.Students, Data.Students.Value);
            transaction.Insert(Tables.Specialities, Data.Specialities.Value);
            transaction.Insert(Tables.EducationContracts, Data.EducationContracts.Value);

            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            transactionCommited = false;
            _logger.LogError(ex, "Something went wrong.");
        }

        stopwatch.Stop();
        _logger.LogInformation("Transaction {transactionCommited}.", transactionCommited ? "commited" : "rollbacked");
        _logger.LogInformation("Data seeding ended. Times spent: [{totalSeconds}]", stopwatch.Elapsed.TotalSeconds);
    }

    private static bool IsAbleToApply(IDbConnection connection)
    {
        bool[] results = Tables.Enumerate().Select(table => connection.ExecuteScalar<bool>($"SELECT count(1) FROM {table}")).ToArray();
        return results.All(e => e is false);
    }
}
