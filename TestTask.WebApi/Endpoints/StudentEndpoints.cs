using TestTask.Application.Services.Interfaces;
using TestTask.WebApi.Extensions;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Endpoints;

public static class StudentEndpoints
{
    private const string Route = $"{Constants.GlobalRoute}/students";

    public static void MapStudentEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(Route);

        group.MapGet("{id}", GetStudentById);
        group.MapPut(string.Empty, UpdateStudent);
        group.MapDelete("{id}", ExpelStudent);
    }

    private static async Task<IResult> GetStudentById(Guid id, IStudentService studentService, CancellationToken cancellationToken)
    {
        var result = await studentService.GetByIdAsync(id, cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.BadRequest(result.ErrorMessage);
    }

    private static async Task<IResult> UpdateStudent(StudentUpdateModel studentUpdateModel, IStudentService studentService, CancellationToken cancellationToken)
    {
        var dto = studentUpdateModel.ToDTO();
        var result = await studentService.UpdateAsync(dto, cancellationToken);
        return result.IsSuccess ? TypedResults.Ok() : TypedResults.BadRequest(result.ErrorMessage);
    }

    private static async Task<IResult> ExpelStudent(Guid id, IStudentService studentService, CancellationToken cancellationToken)
    {
        var result = await studentService.ExpelAsync(id, cancellationToken);
        return result.IsSuccess ? TypedResults.Ok() : TypedResults.BadRequest(result.ErrorMessage);
    }
}
