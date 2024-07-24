using TestTask.DAL.PostgreSQL.Models.Base;

namespace TestTask.DAL.PostgreSQL.Models;

#pragma warning disable IDE1006
#nullable disable
public class Department : PrimaryKeyDataModel
{
    public string title { get; set; }
    public string description { get; set; }
}