using TestTask.DAL.PostgreSQL.Models.Base;

namespace TestTask.DAL.PostgreSQL.Models;

#pragma warning disable IDE1006
#nullable disable
public class Student : PrimaryKeyDataModel
{
    public required Guid group_id { get; init; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public DateOnly birth_date { get; set; }
}

