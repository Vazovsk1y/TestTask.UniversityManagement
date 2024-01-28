using FluentMigrator.Builders.Create.Table;
using TestTask.DAL.Models.Base;

namespace TestTask.DAL.Extensions;

internal static class Extensions
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