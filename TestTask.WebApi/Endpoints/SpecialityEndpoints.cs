using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services.Interfaces;
using TestTask.WebApi.Extensions;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Endpoints;

public static class SpecialityEndpoints
{
    private const string Route = $"{Constants.GlobalRoute}/specialities";

    public static void MapSpecialityEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(Route);

        group.MapGet(string.Empty, GetAllSpecialities);
        group.MapPost(string.Empty, AddSpeciality);
    }

    private static async Task<IResult> GetAllSpecialities([FromServices]ISpecialityService specialityService, CancellationToken cancellationToken)
    {
        var result = await specialityService.GetAllAsync(cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.BadRequest(result.ErrorMessage);
    }

    private static async Task<IResult> AddSpeciality(SpecialityAddModel model, ISpecialityService specialityService, CancellationToken cancellationToken)
    {
        var dto = model.ToDTO();
        var result = await specialityService.AddAsync(dto, cancellationToken);
        return result.IsSuccess ? TypedResults.Created(string.Empty, result.Value) : TypedResults.BadRequest(result.ErrorMessage);
    }
}