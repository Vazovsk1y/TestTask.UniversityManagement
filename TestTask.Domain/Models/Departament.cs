using TestTask.Domain.Common;

namespace TestTask.Domain.Models;

public class Departament : Entity
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    // navigation
    public IReadOnlyCollection<Group> Groups { get; set; } = new List<Group>();
}