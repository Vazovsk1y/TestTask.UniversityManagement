using Dapper;
using TestTask.Application.Contracts;
using TestTask.Application.Services.Interfaces;
using TestTask.Application.Shared;
using TestTask.DAL.Interfaces;
using TestTask.DAL.Models;

namespace TestTask.Application.Services;

internal class StudentService(IDbConnectionFactory dbConnectionFactory) : IStudentService
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<Result<StudentDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _dbConnectionFactory.Create();
        connection.Open();

        var student = await connection.QuerySingleOrDefaultAsync<StudentDTO>(SqlConstants.StudentService.GET_STUDENT_DTO_BY_ID_SQL, new { id });

        if (student is null)
        {
            return Result.Failure<StudentDTO>(Errors.EntityNotFound(nameof(Student)));
        }

        return student;
    }
}
