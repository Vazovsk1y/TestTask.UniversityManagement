namespace TestTask.DAL.Models;

#pragma warning disable IDE1006
#nullable disable
public class Speciality : DataModel
{
    public required Guid id { get; set; }
    public string title { get; set; }
    public string code { get; set; }
}

