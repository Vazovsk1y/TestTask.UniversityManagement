namespace TestTask.DAL.Models;

public class Group
{
    public Guid Id { get; }

    public required Guid DepartamentId { get; init; }

    public required string Title { get; set; }

    // navigation
    public ICollection<Student> Students { get; set; } = new List<Student>();

    public Departament? Departament { get; set; }

    public Group() 
    {
        Id = Guid.NewGuid();
    }
}
