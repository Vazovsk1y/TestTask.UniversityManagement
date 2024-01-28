namespace TestTask.Application.Contracts;

public class StudentDTO
{
    public required Guid Id { get; init; }
    public required Guid DepartamentId { get; init; }
    public required string DepartamentTitle { get; init; }
    public required Guid GroupId { get; init; }
    public required string GroupTitle { get; init; }
    public required DateOnly BirthDate { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DateOnly EducationContractConclusionDate { get; init; }
    public required DateOnly AdmissionDate { get; init; }
    public required DateOnly GraduationDate { get; init; }
    public required string EducationForm { get; init; }
    public required Guid SpecialityId { get; init; }
    public required string SpecialityTitle { get; init; }
    public required string SpecialityCode { get; init; }
}