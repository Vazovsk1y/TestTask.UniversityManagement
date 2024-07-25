using Dapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TestTask.Application.Contracts;
using TestTask.Application.Services.Interfaces;
using TestTask.Application.Shared;
using TestTask.DAL.PostgreSQL.Constants;
using TestTask.DAL.PostgreSQL.Extensions;
using TestTask.DAL.PostgreSQL.Interfaces;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.Application.Services;

internal class StudentService(
    IDbConnectionFactory dbConnectionFactory, 
    IValidator<StudentUpdateDTO> studentUpdateDTOvalidator,
    ILogger<StudentService> logger) : IStudentService
{
    private readonly ILogger _logger = logger;

    public async Task<Result> ExpelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = dbConnectionFactory.Create();
        connection.Open();

        bool studentExists = await connection.IsExistsByIdAsync(Tables.Students, id);
        if (!studentExists)
        {
            return Result.Failure<StudentDTO>(Errors.EntityNotFound(nameof(Student)));
        }

        using var transaction = connection.BeginTransaction();
        try
        {
            await transaction.DeleteByIdAsync(Tables.Students, id);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }

        return Result.Success();
    }

    public async Task<Result<StudentDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = dbConnectionFactory.Create();
        connection.Open();

        var student = await connection.QuerySingleOrDefaultAsync<StudentDTO>(SqlConstants.StudentService.GetStudentDTOByIdSql, new { id });

        return student ?? Result.Failure<StudentDTO>(Errors.EntityNotFound(nameof(Student)));
    }

    public async Task<Result> UpdateAsync(StudentUpdateDTO updateDTO, CancellationToken cancellationToken = default)
    {
        var validationResult = studentUpdateDTOvalidator.Validate(updateDTO);
        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult.ToString());
        }

        using var connection = dbConnectionFactory.Create();
        connection.Open();

        var student = await connection.GetByIdOrDefaultAsync<Student>(Tables.Students, updateDTO.Id);
        if (student is null)
        {
            return Result.Failure(Errors.EntityNotFound(nameof(Student)));
        }

        student.birth_date = updateDTO.BirthDate;
        student.first_name = updateDTO.FirstName.Trim();
        student.last_name = updateDTO.LastName.Trim();

        using var transaction = connection.BeginTransaction();
        try
        {
            await transaction.UpdateByIdAsync(Tables.Students, student);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }

        return Result.Success();
    }
}