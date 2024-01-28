namespace TestTask.DAL.Models;

#pragma warning disable IDE1006
#nullable disable
public class Departament : DataModel
{
    public required Guid id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
}