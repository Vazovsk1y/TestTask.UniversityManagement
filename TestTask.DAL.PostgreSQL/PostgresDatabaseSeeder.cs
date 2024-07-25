using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Diagnostics;
using TestTask.DAL.PostgreSQL.Extensions;
using TestTask.DAL.PostgreSQL.Constants;
using TestTask.DAL.PostgreSQL.Interfaces;

namespace TestTask.DAL.PostgreSQL;

internal class PostgresDatabaseSeeder(
    IDbConnectionFactory connectionFactory, 
    ILogger<PostgresDatabaseSeeder> logger) : IDatabaseSeeder
{
    public void Apply()
    {
        using var connection = connectionFactory.Create();
        connection.Open();

        if (IsAbleToApply(connection))
        {
            Seed(connection);
        }
    }

    private void Seed(IDbConnection connection)
    {
        logger.LogInformation("Data seeding started.");
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        using var transaction = connection.BeginTransaction();
        logger.LogInformation("Transaction began.");

        bool transactionCommited = true;
        try
        {
            transaction.Insert(Tables.Departments, Data.Departments.Value);
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
            logger.LogError(ex, "Something went wrong.");
        }

        stopwatch.Stop();
        logger.LogInformation("Transaction {transactionCommited}.", transactionCommited ? "commited" : "rollbacked");
        logger.LogInformation("Data seeding ended. Times spent: [{totalSeconds}]", stopwatch.Elapsed.TotalSeconds);
    }

    private static bool IsAbleToApply(IDbConnection connection)
    {
        bool[] results = Tables.Enumerate().Select(table => connection.ExecuteScalar<bool>($"SELECT count(1) FROM {table}")).ToArray();
        return results.All(e => e is false);
    }
}
