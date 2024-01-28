namespace TestTask.Application;

public static class Errors
{
    public static string EntityNotFound(string entityName) => $"No {entityName} with the passed id was found.";
}