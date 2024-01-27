using TestTask.Domain.Common;

namespace TestTask.Domain.Models;

public class Group : Entity
{
    public required Guid DepartamentId { get; init; }

    public required string Title { get; set; }

    // navigation
    public IReadOnlyCollection<Student> Students { get; set; } = new List<Student>();

    public Departament? Departament { get; set; }
}
