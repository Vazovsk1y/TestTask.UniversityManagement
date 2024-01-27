using TestTask.Domain.Common;

namespace TestTask.Domain.Models;

public class Student : Entity
{
    public required Guid GroupId { get; init; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required DateTimeOffset BirthDate { get; set; }

    // navigation
    public Group? Group { get; set; }

    public EducationContract? EducationContract { get; set; }
}
