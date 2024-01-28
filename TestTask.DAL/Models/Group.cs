using TestTask.DAL.Models.Base;

namespace TestTask.DAL.Models;

#pragma warning disable IDE1006
#nullable disable
public class Group : PrimaryKeyDataModel
{
    public required Guid departament_id { get; init; }
    public string title { get; set; }
}

