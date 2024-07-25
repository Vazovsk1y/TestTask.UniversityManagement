namespace TestTask.Application.Contracts;

public record DepartmentDTO(
    Guid Id,
    string Title,
    string Description)
{
    public ICollection<GroupDTO> Groups { get; } = new HashSet<GroupDTO>();
}