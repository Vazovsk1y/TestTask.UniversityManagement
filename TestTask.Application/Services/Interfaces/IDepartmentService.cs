using MikesPaging.AspNetCore.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Shared;

namespace TestTask.Application.Services.Interfaces;

public interface IDepartmentService
{
    Task<Result<DepartmentsPage>> GetAsync(PagingOptions pagingOptions, CancellationToken cancellationToken = default);
}