namespace TestTask.DAL.Models;

#pragma warning disable IDE1006
#nullable disable
public class EducationContract : DataModel
{
    public required Guid id { get; set; }
    public required Guid student_id { get; init; }
    public required Guid speciality_id { get; init; }
    public string education_form { get; set; }
    public DateTimeOffset admission_date { get; set; }
    public DateTimeOffset graduation_date { get; set; }
    public DateTimeOffset conclusion_date { get; init; }
}

