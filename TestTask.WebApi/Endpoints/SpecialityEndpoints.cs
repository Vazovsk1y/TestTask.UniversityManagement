using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services.Interfaces;

namespace TestTask.WebApi.Endpoints;

public static class SpecialityEndpoints
{
    public static readonly string Route = $"{Constants.GlobalRoute}/specialities";

    public static void MapSpecialityEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(Route);

        group.MapGet(string.Empty, GetAllSpecialities);
    }

    private static async Task<IResult> GetAllSpecialities([FromServices]ISpecialityService specialityService, CancellationToken cancellationToken)
    {
        var result = await specialityService.GetAllAsync(cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.BadRequest(result.ErrorMessage);
    }
}