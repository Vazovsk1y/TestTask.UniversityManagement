using Dapper;
using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.Application.Services.Interfaces;
using TestTask.Application.Shared;
using TestTask.DAL.PostgreSQL.Constants;
using TestTask.DAL.PostgreSQL.Extensions;
using TestTask.DAL.PostgreSQL.Interfaces;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.Application.Services;

internal class SpecialityService(IDbConnectionFactory connectionFactory, IValidator<SpecialityAddDTO> validator) : ISpecialityService
{
    public async Task<Result<Guid>> AddAsync(SpecialityAddDTO specialityAddDTO, CancellationToken cancellationToken = default)
    {
        var validationResult = validator.Validate(specialityAddDTO);
        if (!validationResult.IsValid)
        {
            return Result.Failure<Guid>(validationResult.ToString());
        }

        using var connection = connectionFactory.Create();
        connection.Open();

        var speciality = new Speciality
        {
            id = Guid.NewGuid(),
            code = specialityAddDTO.Code.Trim(),
            title = specialityAddDTO.Title.Trim(),
        };

        using var transaction = connection.BeginTransaction();
        try
        {
            await transaction.InsertAsync(Tables.Specialities, speciality);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }

        return speciality.id;
    }

    public async Task<Result<IReadOnlyCollection<SpecialityDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = connectionFactory.Create();
        connection.Open();

        var result = await connection.QueryAsync<SpecialityDTO>(SqlConstants.SpecialityService.GetAllSpecialityDTOSql);
        return result.ToList();
    }
}