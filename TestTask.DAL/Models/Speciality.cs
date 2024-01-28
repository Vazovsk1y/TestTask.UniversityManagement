using TestTask.DAL.Models.Base;

namespace TestTask.DAL.Models;

#pragma warning disable IDE1006
#nullable disable
public class Speciality : PrimaryKeyDataModel
{
    public string title { get; set; }
    public string code { get; set; }
}

