using TestTask.Domain.Common;
using TestTask.Domain.Enums;

namespace TestTask.Domain.Models;

public class EducationContract : Entity
{
    public required Guid StudentId { get; init; }

    public required Guid SpecialityId { get; init; }

    public required EducationForms EducationForm { get; set; }

    public required DateTimeOffset AdmissionDate { get; set; }

    public required DateTimeOffset GraduationDate { get; set; }

    public required DateTimeOffset ConclusionDate { get; init; }

    // navigation
    public Student? Student { get; set; }
    public Speciality? Speciality { get; set; }
}
