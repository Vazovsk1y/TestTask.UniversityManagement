using Dapper;
using Dapper.Transaction;
using System.Data;
using System.Reflection;
using TestTask.DAL.Models;

namespace TestTask.DAL.Extensions;

public static class IDbTransactionExtensions
{
    internal static void Insert<T>(this IDbTransaction transaction, string tableTitle, IEnumerable<T> data) where T : DataModel
    {
        var propertiesNames = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => property.Name)
            .ToArray();

        string columnTitles = string.Join(',', propertiesNames);
        string parametersNames = string.Join(',', propertiesNames.Select(e => $"@{e}"));

        string sql = $"INSERT INTO {tableTitle} ({columnTitles}) VALUES ({parametersNames})";

        transaction.Execute(sql, data);
    }
    internal static void Insert<T>(this IDbTransaction transaction, string tableTitle, T data) where T : DataModel
    {
        var propertiesNames = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => property.Name)
            .ToArray();

        string columnTitles = string.Join(',', propertiesNames);
        string parametersNames = string.Join(',', propertiesNames.Select(e => $"@{e}"));

        string sql = $"INSERT INTO {tableTitle} ({columnTitles}) VALUES ({parametersNames})";

        transaction.Execute(sql, data);
    }
    internal static async Task InsertAsync<T>(this IDbTransaction transaction, string tableTitle, IEnumerable<T> data) where T : DataModel
    {
        var propertiesNames = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => property.Name)
            .ToArray();

        string columnTitles = string.Join(',', propertiesNames);
        string parametersNames = string.Join(',', propertiesNames.Select(e => $"@{e}"));

        string sql = $"INSERT INTO {tableTitle} ({columnTitles}) VALUES ({parametersNames})";

        await transaction.ExecuteAsync(sql, data);
    }
    internal static async Task InsertAsync<T>(this IDbTransaction transaction, string tableTitle, T data) where T : DataModel
    {
        var propertiesNames = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => property.Name)
            .ToArray();

        string columnTitles = string.Join(',', propertiesNames);
        string parametersNames = string.Join(',', propertiesNames.Select(e => $"@{e}"));

        string sql = $"INSERT INTO {tableTitle} ({columnTitles}) VALUES ({parametersNames})";

        await transaction.ExecuteAsync(sql, data);
    }
}