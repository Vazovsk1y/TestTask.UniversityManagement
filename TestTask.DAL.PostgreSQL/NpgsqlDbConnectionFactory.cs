using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using TestTask.DAL.PostgreSQL.Interfaces;

namespace TestTask.DAL.PostgreSQL;

internal class NpgsqlDbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    public IDbConnection Create()
    {
        string connectionString = configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string is not found.");
        return new NpgsqlConnection(connectionString);
    }
}
