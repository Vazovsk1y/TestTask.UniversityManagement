using MikesPaging.AspNetCore.Common;

namespace TestTask.Application.Contracts;

public record DepartamentsPage : Page<DepartamentDTO>
{
    public DepartamentsPage(IReadOnlyCollection<DepartamentDTO> departaments, int totalDepartamentsCount, PagingOptions? pagingOptions) : base(departaments, totalDepartamentsCount, pagingOptions)
    {
    }
}