using Dapper;
using Dapper.Transaction;
using System.Data;
using System.Reflection;
using TestTask.DAL.Models.Base;

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

    public static async Task UpdateByIdAsync<T>(this IDbTransaction transaction, string tableTitle, T data) where T : PrimaryKeyDataModel
    {
        var propertiesNames = typeof(T)
           .GetProperties(BindingFlags.Public | BindingFlags.Instance)
           .Select(property => property.Name)
           .ToList();

        propertiesNames.Remove(nameof(data.id));

        string setStatements = string.Join($",{Environment.NewLine}", propertiesNames.Select(name => $"{name} = @{name}"));
        string sql = $"UPDATE {tableTitle} SET \n{setStatements} \nWHERE {nameof(data.id)} = @{nameof(data.id)}";

        await transaction.ExecuteAsync(sql, data);
    }

    public static async Task UpdateByAsync<T>(this IDbTransaction transaction, string tableTitle, string byColumnName, T data) where T : DataModel
    {
        var propertiesNames = typeof(T)
           .GetProperties(BindingFlags.Public | BindingFlags.Instance)
           .Select(property => property.Name)
           .ToList();

        if (!propertiesNames.Contains(byColumnName))
        {
            throw new ArgumentException("Invalid column name.");
        }

        propertiesNames.Remove(byColumnName);

        string setStatements = string.Join($",{Environment.NewLine}", propertiesNames.Select(name => $"{name} = @{name}"));
        string sql = $"UPDATE {tableTitle} SET \n{setStatements} \nWHERE {byColumnName} = @{byColumnName}";

        await transaction.ExecuteAsync(sql, data);
    }
}