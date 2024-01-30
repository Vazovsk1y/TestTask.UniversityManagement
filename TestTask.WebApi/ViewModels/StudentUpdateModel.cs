namespace TestTask.WebApi.ViewModels;

public record StudentUpdateModel(
    Guid Id, 
    string FirstName, 
    string LastName, 
    DateTime BirthDate);
