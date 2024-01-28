
using TestTask.Application.Services.Interfaces;

namespace TestTask.WebApi.Endpoints;

public static class StudentEndpoints
{
    public static readonly string Route = $"{Constants.GlobalRoute}/students";

    public static void MapStudentEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(Route);

        group.MapGet("{id}", GetStudentById);
    }

    private static async Task<IResult> GetStudentById(Guid id, IStudentService studentService, CancellationToken cancellationToken)
    {
        var result = await studentService.GetByIdAsync(id, cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.BadRequest(result.ErrorMessage);
    }
}
