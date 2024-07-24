using Dapper;
using MikesPaging.AspNetCore.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Services.Interfaces;
using TestTask.Application.Shared;
using TestTask.DAL.PostgreSQL.Constants;
using TestTask.DAL.PostgreSQL.Interfaces;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.Application.Services;

internal class DepartamentService(IDbConnectionFactory dbConnectionFactory) : IDepartamentService
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<Result<DepartamentsPage>> GetAsync(PagingOptions pagingOptions, CancellationToken cancellationToken = default)
    {
        using var connection = _dbConnectionFactory.Create();
        connection.Open();

        int totalDepartamentsCount = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(d.{nameof(Department.id)}) FROM {Tables.Departments} AS d");
        int limit = pagingOptions.PageSize;
        int offset = (pagingOptions.PageIndex - 1) * pagingOptions.PageSize;

        var lookup = new Dictionary<Guid, DepartamentDTO>();
        var result = await connection.QueryAsync<DepartamentDTO, GroupDTO, DepartamentDTO>(
        SqlConstants.DepartamentService.GET_ALL_DEPARTAMENT_DTO_WITH_PAGING_SQL,
        (departament, group) =>
        {
            if (!lookup.TryGetValue(departament.Id, out var departamentDTO))
            {
                departamentDTO = departament;
                lookup.Add(departamentDTO.Id, departamentDTO);
            }

            departamentDTO.Groups.Add(group);
            return departamentDTO;
        },
        new { offset, limit },
        splitOn: nameof(GroupDTO.GroupId));

        return new DepartamentsPage(lookup.Values, totalDepartamentsCount, pagingOptions);
    }
}