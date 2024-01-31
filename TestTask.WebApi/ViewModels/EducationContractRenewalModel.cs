namespace TestTask.WebApi.ViewModels;

public record EducationContractRenewalModel(
    Guid StudentId,
    Guid SpecialityId,
    DateTime AdmissionDate,
    DateTime GraduationDate,
    string EducationForm);