using TestTask.Domain.Common;

namespace TestTask.Domain.Models;

public class Speciality : Entity
{
    public required string Title { get; set; }

    public required string Code { get; set; }
}
