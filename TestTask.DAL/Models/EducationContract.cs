using TestTask.DAL.Models.Base;

namespace TestTask.DAL.Models;

#pragma warning disable IDE1006
#nullable disable
public class EducationContract : DataModel
{
    public required Guid student_id { get; init; }
    public required Guid speciality_id { get; init; }
    public string education_form { get; set; }
    public DateOnly admission_date { get; set; }
    public DateOnly graduation_date { get; set; }
    public DateOnly conclusion_date { get; init; }
}

