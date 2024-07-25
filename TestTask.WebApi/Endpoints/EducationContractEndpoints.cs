using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services.Interfaces;
using TestTask.WebApi.Extensions;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Endpoints;

public static class EducationContractEndpoints
{
    private const string Route = $"{Constants.GlobalRoute}/education-contracts";

    public static void MapEducationContractEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(Route);

        group.MapPut(string.Empty, RenewalEducationContract);
    }

    private static async Task<IResult> RenewalEducationContract(EducationContractRenewalModel model, [FromServices]IEducationContractService educationContractService, CancellationToken cancellationToken)
    {
        var dto = model.ToDTO();
        var result = await educationContractService.RenewalAsync(dto, cancellationToken);

        return result.IsSuccess ? TypedResults.Ok() : TypedResults.BadRequest(result.ErrorMessage);
    }
}