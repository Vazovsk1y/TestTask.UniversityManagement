namespace TestTask.DAL.Models;

public class Departament
{
    public Guid Id { get; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    // navigation
    public ICollection<Group> Groups { get; set; } = new List<Group>();

    public Departament()
    {
        Id = Guid.NewGuid();
    }
}