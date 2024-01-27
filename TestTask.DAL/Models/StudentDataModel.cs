namespace TestTask.DAL.Models;

#pragma warning disable IDE1006
#nullable disable
public class StudentDataModel : DataModel
{
    public required Guid id { get; set; }
    public required Guid group_id { get; init; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public DateTimeOffset birth_date { get; set; }
}

