using FluentMigrator.Builders.Create.Table;
using TestTask.DAL.PostgreSQL.Models.Base;

namespace TestTask.DAL.PostgreSQL.Extensions;

internal static class Common
{
    internal static ICreateTableColumnOptionOrWithColumnSyntax WithPrimaryKeyColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
    {
        return tableWithColumnSyntax
            .WithColumn(nameof(PrimaryKeyDataModel.id))
            .AsGuid()
            .NotNullable()
            .PrimaryKey();
    }
}