using MikesPaging.AspNetCore.Common;

namespace TestTask.Application.Contracts;

public record DepartmentsPage : Page<DepartmentDTO>
{
    public DepartmentsPage(IReadOnlyCollection<DepartmentDTO> departments, int totalDepartmentsCount, PagingOptions? pagingOptions) : base(departments, totalDepartmentsCount, pagingOptions)
    {
    }
}