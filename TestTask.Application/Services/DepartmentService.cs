using Dapper;
using MikesPaging.AspNetCore.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Services.Interfaces;
using TestTask.Application.Shared;
using TestTask.DAL.PostgreSQL.Constants;
using TestTask.DAL.PostgreSQL.Interfaces;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.Application.Services;

internal class DepartmentService(IDbConnectionFactory dbConnectionFactory) : IDepartmentService
{
    public async Task<Result<DepartmentsPage>> GetAsync(PagingOptions pagingOptions, CancellationToken cancellationToken = default)
    {
        using var connection = dbConnectionFactory.Create();
        connection.Open();

        int totalDepartmentsCount = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(d.{nameof(Department.id)}) FROM {Tables.Departments} AS d");
        int limit = pagingOptions.PageSize;
        int offset = (pagingOptions.PageIndex - 1) * pagingOptions.PageSize;

        var lookup = new Dictionary<Guid, DepartmentDTO>();
        _ = await connection.QueryAsync<DepartmentDTO, GroupDTO, DepartmentDTO>(
        SqlConstants.DepartmentService.GetAllDepartmentDTOWithPagingSql,
        (department, group) =>
        {
            if (!lookup.TryGetValue(department.Id, out var departmentDTO))
            {
                departmentDTO = department;
                lookup.Add(departmentDTO.Id, departmentDTO);
            }

            departmentDTO.Groups.Add(group);
            return departmentDTO;
        },
        new { offset, limit },
        splitOn: nameof(GroupDTO.GroupId));

        return new DepartmentsPage(lookup.Values, totalDepartmentsCount, pagingOptions);
    }
}