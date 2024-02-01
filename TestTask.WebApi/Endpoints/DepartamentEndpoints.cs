using Microsoft.AspNetCore.Mvc;
using MikesPaging.AspNetCore;
using MikesPaging.AspNetCore.Common.ViewModels;
using TestTask.Application.Services.Interfaces;

namespace TestTask.WebApi.Endpoints;

public static class DepartamentEndpoints
{
    public static readonly string Route = $"{Constants.GlobalRoute}/departaments";

    public static void MapDepartamentEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(Route);

        group.MapGet(string.Empty, GetDepartamentsPage);
    }

    private static async Task<IResult> GetDepartamentsPage([AsParameters]PagingOptionsModel model, [FromServices]IDepartamentService departamentService, CancellationToken cancellationToken)
    {
        var dtoResult = model.ToOptions();
        if (dtoResult.IsFailure)
        {
            return TypedResults.BadRequest(dtoResult.ErrorMessage);
        }

        var pagingOptions = dtoResult.Value;
        var pageResult = await departamentService.GetAsync(pagingOptions, cancellationToken);

        return pageResult.IsSuccess ? TypedResults.Ok(pageResult.Value) : TypedResults.BadRequest(pageResult.ErrorMessage);
    }
}