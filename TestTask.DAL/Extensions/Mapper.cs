using TestTask.DAL.Models;
using TestTask.Domain.Enums;
using TestTask.Domain.Models;

namespace TestTask.DAL.Extensions;

internal static class Mapper
{
    internal static Departament ToDomain(DepartamentDataModel dataModel)
    {
        return new Departament
        {
            Id = dataModel.id,
            Title = dataModel.title,
            Description = dataModel.description,
        };
    }

    public static Group ToDomain(GroupDataModel dataModel)
    {
        return new Group
        {
            Id = dataModel.id,
            DepartamentId = dataModel.departament_id,
            Title = dataModel.title,
        };
    }

    internal static Student ToDomain(StudentDataModel dataModel)
    {
        return new Student
        {
            Id = dataModel.id,
            GroupId = dataModel.group_id,
            FirstName = dataModel.first_name,
            LastName = dataModel.last_name,
            BirthDate = dataModel.birth_date,
        };
    }

    internal static EducationContract ToDomain(EducationContractDataModel dataModel)
    {
        return new EducationContract
        {
            Id = dataModel.id,
            StudentId = dataModel.student_id,
            SpecialityId = dataModel.speciality_id,
            EducationForm = Enum.Parse<EducationForms>(dataModel.education_form),
            AdmissionDate = dataModel.admission_date,
            GraduationDate = dataModel.graduation_date,
            ConclusionDate = dataModel.conclusion_date,
        };
    }

    internal static Speciality ToDomain(SpecialityDataModel dataModel)
    {
        return new Speciality
        {
            Id = dataModel.id,
            Title = dataModel.title,
            Code = dataModel.code
        };
    }
}