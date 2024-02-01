namespace TestTask.Application.Contracts;

public record DepartamentDTO(
    Guid Id,
    string Title,
    string Description)
{
    public ICollection<GroupDTO> Groups { get; } = new HashSet<GroupDTO>();
}