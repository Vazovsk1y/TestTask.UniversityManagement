namespace TestTask.DAL.Models;

public class Student
{
    public Guid Id { get; }

    public required Guid GroupId { get; init; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required DateTimeOffset BirthDate { get; set; }

    // navigation
    public Group? Group { get; set; }

    public EducationContract? EducationContract { get; set; }

    public Student()
    {
        Id = Guid.NewGuid();
    }
}
