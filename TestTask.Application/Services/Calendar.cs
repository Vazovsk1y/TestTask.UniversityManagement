using TestTask.Application.Services.Interfaces;

namespace TestTask.Application.Services;

public class Calendar : ICalendar
{
    public DateOnly Today()
    {
        return DateOnly.FromDateTime(DateTime.UtcNow);
    }
}