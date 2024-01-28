using TestTask.DAL.Models.Base;

namespace TestTask.DAL.Models;

#pragma warning disable IDE1006
#nullable disable
public class Departament : PrimaryKeyDataModel
{
    public string title { get; set; }
    public string description { get; set; }
}