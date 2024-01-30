namespace TestTask.Application.Contracts;

public record StudentUpdateDTO(
    Guid Id,
    string FirstName, 
    string LastName,
    DateOnly BirthDate);