using TestTask.Application.Contracts;
using TestTask.Application.Shared;

namespace TestTask.Application.Services.Interfaces;

public interface IEducationContractService
{
    Task<Result> RenewalAsync(EducationContractRenewalDTO renewalDTO, CancellationToken cancellationToken = default);
}