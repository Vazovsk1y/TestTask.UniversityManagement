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

    public static EducationContractRenewalDTO ToDTO(this EducationContractRenewalModel model)
    {
        return new EducationContractRenewalDTO(
            model.StudentId, 
            model.SpecialityId, 
            DateOnly.FromDateTime(model.AdmissionDate), 
            DateOnly.FromDateTime(model.GraduationDate), 
            model.EducationForm);
    }
}


