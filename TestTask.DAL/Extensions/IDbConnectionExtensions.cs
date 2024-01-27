using Dapper;
using Dapper.Transaction;
using System.Data;
using System.Reflection;
using TestTask.DAL.Models;

namespace TestTask.DAL.Extensions;

internal static class IDbConnectionExtensions
{
    internal static void Insert<T>(this IDbConnection connection, string tableTitle, IEnumerable<T> data, IDbTransaction? transaction = null) where T : DataModel
    {
        var propertiesNames = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => property.Name)
            .ToArray();

        string columnTitles = string.Join(',', propertiesNames);
        string parametersNames = string.Join(',', propertiesNames.Select(e => $"@{e}"));

        string sql = $"INSERT INTO {tableTitle} ({columnTitles}) VALUES ({parametersNames})";

        connection.Execute(sql, data, transaction);
    }
    internal static void Insert<T>(this IDbConnection connection, string tableTitle, T data, IDbTransaction? transaction = null) where T : DataModel
    {
        var propertiesNames = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => property.Name)
            .ToArray();

        string columnTitles = string.Join(',', propertiesNames);
        string parametersNames = string.Join(',', propertiesNames.Select(e => $"@{e}"));

        string sql = $"INSERT INTO {tableTitle} ({columnTitles}) VALUES ({parametersNames})";

        connection.Execute(sql, data, transaction);
    }
    internal static async Task InsertAsync<T>(this IDbConnection connection, string tableTitle, IEnumerable<T> data, IDbTransaction? transaction = null) where T : DataModel
    {
        var propertiesNames = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => property.Name)
            .ToArray();

        string columnTitles = string.Join(',', propertiesNames);
        string parametersNames = string.Join(',', propertiesNames.Select(e => $"@{e}"));

        string sql = $"INSERT INTO {tableTitle} ({columnTitles}) VALUES ({parametersNames})";

        await connection.ExecuteAsync(sql, data, transaction);
    }
    internal static async Task InsertAsync<T>(this IDbConnection connection, string tableTitle, T data, IDbTransaction? transaction = null) where T : DataModel
    {
        var propertiesNames = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => property.Name)
            .ToArray();

        string columnTitles = string.Join(',', propertiesNames);
        string parametersNames = string.Join(',', propertiesNames.Select(e => $"@{e}"));

        string sql = $"INSERT INTO {tableTitle} ({columnTitles}) VALUES ({parametersNames})";

        await connection.ExecuteAsync(sql, data, transaction);
    }
}