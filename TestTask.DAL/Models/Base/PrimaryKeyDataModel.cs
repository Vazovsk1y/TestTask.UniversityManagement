namespace TestTask.DAL.Models.Base;

#pragma warning disable IDE1006
public class PrimaryKeyDataModel : DataModel
{
    public required Guid id { get; init; }
}