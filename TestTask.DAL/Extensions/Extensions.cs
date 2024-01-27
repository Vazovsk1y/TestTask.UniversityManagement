using FluentMigrator.Builders.Create.Table;

namespace TestTask.DAL.Extensions;

internal static class Extensions
{
    internal static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
    {
        return tableWithColumnSyntax
            .WithColumn("id")
            .AsGuid()
            .NotNullable()
            .PrimaryKey();
    }
}