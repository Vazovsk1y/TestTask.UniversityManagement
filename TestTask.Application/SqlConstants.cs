using TestTask.Application.Contracts;
using TestTask.DAL.PostgreSQL.Constants;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.Application;

public static class SqlConstants
{
    public static class StudentService
    {
        public static readonly string GET_STUDENT_DTO_BY_ID_SQL = $@"
               SELECT 
                   s.{nameof(Student.id)} AS {nameof(StudentDTO.Id)}, 
                   d.{nameof(Department.id)} AS {nameof(StudentDTO.DepartamentId)}, 
                   d.{nameof(Department.title)} AS {nameof(StudentDTO.DepartamentTitle)}, 
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
               INNER JOIN {Tables.Departments} d ON g.{nameof(Group.departament_id)} = d.{nameof(Department.id)}
               INNER JOIN {Tables.EducationContracts} ec ON s.{nameof(Student.id)} = ec.{nameof(EducationContract.student_id)}
               INNER JOIN {Tables.Specialities} sp ON ec.{nameof(EducationContract.speciality_id)} = sp.{nameof(Speciality.id)}
               WHERE s.{nameof(Student.id)} = @id";
    }

    public static class SpecialityService
    {
        public static readonly string GET_ALL_SPECIALITY_DTO_SQL = $@"
               SELECT 
                   sp.{nameof(Speciality.id)} AS {nameof(SpecialityDTO.Id)}, 
                   sp.{nameof(Speciality.title)} AS {nameof(SpecialityDTO.Title)}, 
                   sp.{nameof(Speciality.code)} AS {nameof(SpecialityDTO.Code)}
               FROM {Tables.Specialities} sp";
    }

    public static class DepartamentService
    {
        public static readonly string GET_ALL_DEPARTAMENT_DTO_WITH_PAGING_SQL = $@"
              SELECT 
                  d.{nameof(Department.id)} AS {nameof(DepartamentDTO.Id)},
                  d.{nameof(Department.title)} AS {nameof(DepartamentDTO.Title)},
                  d.{nameof(Department.description)} AS {nameof(DepartamentDTO.Description)},
                  g.{nameof(Group.id)} AS {nameof(GroupDTO.GroupId)},
                  g.{nameof(Group.title)} AS {nameof(GroupDTO.Title)},
                  COUNT(s.{nameof(Student.id)}) AS {nameof(GroupDTO.StudentsCount)}
              FROM (
                  SELECT 
                      d.{nameof(Department.id)},
                      d.{nameof(Department.title)},
                      d.{nameof(Department.description)}
                  FROM {Tables.Departments} as d
                  ORDER BY d.{nameof(Department.title)}
                  OFFSET @offset ROWS
                  FETCH NEXT @limit ROWS ONLY
              ) AS d
              LEFT JOIN {Tables.Groups} as g ON g.{nameof(Group.departament_id)} = d.{nameof(Department.id)}
              LEFT JOIN {Tables.Students} as s ON s.{nameof(Student.group_id)} = g.{nameof(Group.id)}
              GROUP BY g.{nameof(Group.id)}, d.{nameof(Department.id)}, d.{nameof(Department.title)}, d.{nameof(Department.description)}
              ORDER BY d.{nameof(Department.title)}";
    }
}