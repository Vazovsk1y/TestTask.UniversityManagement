namespace TestTask.DAL.Models;

public class Speciality
{
    public Guid Id { get; }

    public required string Title { get; set; }

    public required string Code { get; set; }

    public Speciality() 
    {
        Id = Guid.NewGuid();
    }
}
