using Dapper;
using Dapper.Transaction;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using TestTask.DAL.Models.Base;

namespace TestTask.DAL.Extensions;

public static class IDbConnectionExtensions
{
    public static void Insert<T>(this IDbConnection connection, string tableTitle, IEnumerable<T> data, IDbTransaction? transaction = null) where T : DataModel
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

    public static void Insert<T>(this IDbConnection connection, string tableTitle, T data, IDbTransaction? transaction = null) where T : DataModel
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

    public static async Task InsertAsync<T>(this IDbConnection connection, string tableTitle, IEnumerable<T> data, IDbTransaction? transaction = null) where T : DataModel
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

    public static async Task InsertAsync<T>(this IDbConnection connection, string tableTitle, T data, IDbTransaction? transaction = null) where T : DataModel
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

    public static async Task<T?> GetByIdOrDefaultAsync<T>(this IDbConnection connection, string tableTitle, Guid id, Expression<Func<T, object[]>> selector) where T : DataModel
    {
        if (selector.Body is NewArrayExpression memberInitExpression)
        {
            var columns = memberInitExpression.Expressions.Select(GetPropertyNameFromExpression).ToList();
            string columnTitlesRaw = string.Join(',', columns);
            string sql = $"SELECT {columnTitlesRaw} FROM {tableTitle} WHERE {nameof(PrimaryKeyDataModel.id)} = @id";

            return await connection.QuerySingleOrDefaultAsync<T>(sql, new { id });
        }

        throw new ArgumentException("Invalid selector");
    }

    public static async Task<T> GetByIdAsync<T>(this IDbConnection connection, string tableTitle, Guid id, Expression<Func<T, object[]>> selector) where T : DataModel
    {
        if (selector.Body is NewArrayExpression memberInitExpression)
        {
            var columns = memberInitExpression.Expressions.Select(GetPropertyNameFromExpression).ToList();
            string columnTitlesRaw = string.Join(',', columns);
            string sql = $"SELECT {columnTitlesRaw} FROM {tableTitle} WHERE {nameof(PrimaryKeyDataModel.id)} = @id";

            return await connection.QuerySingleAsync<T>(sql, new { id });
        }

        throw new ArgumentException("Invalid selector.");
    }

    public static async Task<T?> GetByIdOrDefaultAsync<T>(this IDbConnection connection, string tableTitle, Guid id) where T : DataModel
    {
        var propertiesNames = typeof(T)
           .GetProperties(BindingFlags.Public | BindingFlags.Instance)
           .Select(property => property.Name)
           .ToArray();

        string columnTitlesRaw = string.Join(',', propertiesNames);
        string sql = $"SELECT {columnTitlesRaw} FROM {tableTitle} WHERE {nameof(PrimaryKeyDataModel.id)}  = @id";

        return await connection.QuerySingleOrDefaultAsync<T>(sql, new { id });
    }

    public static async Task<T> GetByIdAsync<T>(this IDbConnection connection, string tableTitle, Guid id) where T : DataModel
    {
        var propertiesNames = typeof(T)
           .GetProperties(BindingFlags.Public | BindingFlags.Instance)
           .Select(property => property.Name)
           .ToArray();

        string columnTitlesRaw = string.Join(',', propertiesNames);
        string sql = $"SELECT {columnTitlesRaw} FROM {tableTitle} WHERE {nameof(PrimaryKeyDataModel.id)}  = @id";

        return await connection.QuerySingleAsync<T>(sql, new { id });
    }

    private static string GetPropertyNameFromExpression(Expression expression)
    {
        if (expression is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        if (expression is MemberExpression memberExpressionWithoutConversion)
        {
            return memberExpressionWithoutConversion.Member.Name;
        }

        throw new ArgumentException("Invalid expression.");
    }
}