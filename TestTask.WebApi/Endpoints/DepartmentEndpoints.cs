using Microsoft.AspNetCore.Mvc;
using MikesPaging.AspNetCore;
using MikesPaging.AspNetCore.Common.ViewModels;
using TestTask.Application.Services.Interfaces;

namespace TestTask.WebApi.Endpoints;

public static class DepartmentEndpoints
{
    private const string Route = $"{Constants.GlobalRoute}/departments";

    public static void MapDepartmentEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(Route);

        group.MapGet(string.Empty, GetDepartmentsPage);
    }

    private static async Task<IResult> GetDepartmentsPage([AsParameters]PagingOptionsModel model, [FromServices]IDepartmentService departmentService, CancellationToken cancellationToken)
    {
        var dtoResult = model.ToOptions();
        if (dtoResult.IsFailure)
        {
            return TypedResults.BadRequest(dtoResult.ErrorMessage);
        }

        var pagingOptions = dtoResult.Value;
        var pageResult = await departmentService.GetAsync(pagingOptions, cancellationToken);

        return pageResult.IsSuccess ? TypedResults.Ok(pageResult.Value) : TypedResults.BadRequest(pageResult.ErrorMessage);
    }
}