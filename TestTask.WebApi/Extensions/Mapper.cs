using TestTask.Application.Contracts;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Extensions;

public static class Mapper
{
    public static StudentUpdateDTO ToDTO(this StudentUpdateModel studentUpdateModel)
    {
        return new StudentUpdateDTO(
            studentUpdateModel.Id, 
            studentUpdateModel.FirstName, 
            studentUpdateModel.LastName, 
            DateOnly.FromDateTime(studentUpdateModel.BirthDate));
    }
}


