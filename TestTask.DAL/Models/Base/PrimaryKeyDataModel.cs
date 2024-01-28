namespace TestTask.DAL.Models.Base;

#pragma warning disable IDE1006
public abstract class PrimaryKeyDataModel : DataModel
{
    public Guid id { get; init; }
}