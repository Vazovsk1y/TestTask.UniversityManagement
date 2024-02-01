using TestTask.Application.Contracts;
using TestTask.Application.Shared;

namespace TestTask.Application.Services.Interfaces;

public interface ISpecialityService
{
    Task<Result<IReadOnlyCollection<SpecialityDTO>>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Result<Guid>> AddAsync(SpecialityAddDTO specialityAddDTO, CancellationToken cancellationToken = default);
}
