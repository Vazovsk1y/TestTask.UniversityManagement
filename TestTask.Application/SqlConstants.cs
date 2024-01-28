using TestTask.Application.Contracts;
using TestTask.DAL.Constants;
using TestTask.DAL.Models;

namespace TestTask.Application;

public static class SqlConstants
{
    public static class StudentService
    {
        public static readonly string GET_STUDENT_DTO_BY_ID_SQL = $@"
               SELECT 
                   s.{nameof(Student.id)} AS {nameof(StudentDTO.Id)}, 
                   d.{nameof(Departament.id)} AS {nameof(StudentDTO.DepartamentId)}, 
                   d.{nameof(Departament.title)} AS {nameof(StudentDTO.DepartamentTitle)}, 
                   g.{nameof(Group.id)} AS {nameof(StudentDTO.GroupId)}, 
                   g.{nameof(Group.title)} AS {nameof(StudentDTO.GroupTitle)}, 
                   s.{nameof(Student.birth_date)} AS {nameof(StudentDTO.BirthDate)}, 
                   s.{nameof(Student.first_name)} AS {nameof(StudentDTO.FirstName)},
                   s.{nameof(Student.last_name)} AS {nameof(StudentDTO.LastName)},
                   ec.{nameof(EducationContract.conclusion_date)} AS {nameof(StudentDTO.EducationContractConclusionDate)}, 
                   ec.{nameof(EducationContract.admission_date)} AS {nameof(StudentDTO.AdmissionDate)}, 
                   ec.{nameof(EducationContract.graduation_date)} AS {nameof(StudentDTO.GraduationDate)}, 
                   ec.{nameof(EducationContract.education_form)} AS {nameof(StudentDTO.EducationForm)},
                   sp.{nameof(Speciality.id)} AS {nameof(StudentDTO.SpecialityId)},
                   sp.{nameof(Speciality.title)} AS {nameof(StudentDTO.SpecialityTitle)},
                   sp.{nameof(Speciality.code)} AS {nameof(StudentDTO.SpecialityCode)}
               FROM {Tables.Students} s
               INNER JOIN {Tables.Groups} g ON s.{nameof(Student.group_id)} = g.{nameof(Group.id)}
               INNER JOIN {Tables.Departaments} d ON g.{nameof(Group.departament_id)} = d.{nameof(Departament.id)}
               INNER JOIN {Tables.EducationContracts} ec ON s.{nameof(Student.id)} = ec.{nameof(EducationContract.student_id)}
               INNER JOIN {Tables.Specialities} sp ON ec.{nameof(EducationContract.speciality_id)} = sp.{nameof(Speciality.id)}
               WHERE s.{nameof(Student.id)} = @id";
    }
}