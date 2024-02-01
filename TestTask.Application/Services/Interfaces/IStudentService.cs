using TestTask.Application.Contracts;
using TestTask.Application.Shared;

namespace TestTask.Application.Services.Interfaces;

public interface IStudentService 
{
    Task<Result<StudentDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(StudentUpdateDTO updateDTO, CancellationToken cancellationToken = default);

    Task<Result> ExpelAsync(Guid id, CancellationToken cancellationToken = default);
}