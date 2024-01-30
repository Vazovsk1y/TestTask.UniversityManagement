using Dapper;
using TestTask.Application.Contracts;
using TestTask.Application.Services.Interfaces;
using TestTask.Application.Shared;
using TestTask.DAL.Interfaces;

namespace TestTask.Application.Services;

internal class SpecialityService(IDbConnectionFactory connectionFactory) : ISpecialityService
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async Task<Result<IReadOnlyCollection<SpecialityDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.Create();
        connection.Open();

        var result = await connection.QueryAsync<SpecialityDTO>(SqlConstants.SpecialityService.GET_ALL_SPECIALITY_DTO_SQL);
        return result.ToList();
    }
}