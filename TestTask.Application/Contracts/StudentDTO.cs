namespace TestTask.Application.Contracts;

public record StudentDTO(
    Guid Id, 
    Guid DepartamentId, 
    string DepartamentTitle, 
    Guid GroupId, 
    string GroupTitle, 
    DateOnly BirthDate, 
    string FirstName, 
    string LastName, 
    DateOnly EducationContractConclusionDate, 
    DateOnly AdmissionDate, 
    DateOnly GraduationDate, 
    string EducationForm, 
    Guid SpecialityId, 
    string SpecialityTitle, 
    string SpecialityCode);
