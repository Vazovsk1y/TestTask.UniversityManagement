namespace TestTask.DAL.Models;

#pragma warning disable IDE1006
#nullable disable
public class SpecialityDataModel : DataModel
{
    public required Guid id { get; set; }
    public string title { get; set; }
    public string code { get; set; }
}

