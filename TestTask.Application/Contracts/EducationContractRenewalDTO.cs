namespace TestTask.Application.Contracts;

public record EducationContractRenewalDTO(
    Guid StudentId,
    Guid SpecialityId,
    DateOnly AdmissionDate,
    DateOnly GraduationDate,
    string EducationForm);