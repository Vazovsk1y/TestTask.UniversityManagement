using Dapper;
using Dapper.Transaction;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using TestTask.DAL.Models.Base;

namespace TestTask.DAL.Extensions;

public static class IDbConnectionExtensions
{
    public static async Task<bool> IsExistsByAsync<T>(this IDbConnection connection, string tableTitle, string byColumnName, T byValue)
    {
        string sql = $"SELECT count(1) FROM {tableTitle} WHERE {byColumnName}=@{byColumnName}";
        var parametrs = new DynamicParameters();
        parametrs.Add($"{byColumnName}", byValue);

        return await connection.ExecuteScalarAsync<bool>(sql, parametrs);
    }
    public static async Task<T?> GetByOrDefaultAsync<T, TBy>(this IDbConnection connection, string tableTitle, string byColumnName, TBy byValue) where T : DataModel
    {
        var propertiesNames = typeof(T)
           .GetProperties(BindingFlags.Public | BindingFlags.Instance)
           .Select(property => property.Name)
           .ToArray();

        string columnTitlesRaw = string.Join(',', propertiesNames);
        string sql = $"SELECT {columnTitlesRaw} FROM {tableTitle} WHERE {byColumnName}=@{byColumnName}";

        var parametrs = new DynamicParameters();
        parametrs.Add($"{byColumnName}", byValue);

        return await connection.QuerySingleOrDefaultAsync<T>(sql, parametrs);
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