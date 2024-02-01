namespace TestTask.Application.Contracts;

public record GroupDTO(
    Guid GroupId,
    string Title,
    long StudentsCount);