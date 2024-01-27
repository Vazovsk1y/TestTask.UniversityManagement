using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using TestTask.DAL.Interfaces;

namespace TestTask.DAL;

internal class NpgsqlDbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly IConfiguration _configuration = configuration;

    public IDbConnection Create()
    {
        string connectionString = _configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string is not found.");
        return new NpgsqlConnection(connectionString);
    }
}
