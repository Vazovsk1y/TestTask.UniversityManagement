namespace TestTask.DAL.Models;

#pragma warning disable IDE1006
#nullable disable
public class GroupDataModel : DataModel
{
    public required Guid id { get; set; }
    public required Guid departament_id { get; init; }
    public string title { get; set; }
}

