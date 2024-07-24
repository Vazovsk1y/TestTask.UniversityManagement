using Bogus;
using TestTask.DAL.PostgreSQL.Constants;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.DAL.PostgreSQL;

internal static class Data
{
    private const int DepartamentsCount = 15;

    private const int GroupsCount = 30;

    private const int StudentsCount = 300;

    private const int SpecialitiesCount = 50;

    private static readonly Faker<Department> DepartmentFaker = new Faker<Department>()
       .RuleFor(e => e.id, r => Guid.NewGuid())
       .RuleFor(e => e.title, r => 
       {
           var value = $"{r.Company.CompanyName()}_{r.UniqueIndex}";
           int maxLength = Constraints.Department.MaxTitleLength;
           return value.Length <= maxLength? value : value[..maxLength]; 
       })
       .RuleFor(e => e.description, r =>
       {
           var value = r.Lorem.Paragraph();
           int maxLength = Constraints.Department.MaxDescriptionLength;
           return value.Length <= maxLength ? value : value[..maxLength];
       });

    public static readonly Lazy<IReadOnlyCollection<Department>> Departments = new(Enumerable.Range(0, DepartamentsCount).Select(e => DepartmentFaker.Generate()).ToList());

    private static readonly Faker<Group> GroupFaker = new Faker<Group>()
       .RuleFor(e => e.id, r => Guid.NewGuid())
       .RuleFor(e => e.title, r =>
       {
           var value = $"{r.Company.CompanyName()}_{r.UniqueIndex}";
           int maxLength = Constraints.Group.MaxTitleLength;
           return value.Length <= maxLength ? value : value[..maxLength];
       })
       .RuleFor(e => e.departament_id, r => r.PickRandom(Departments.Value.Select(e => e.id)));

    public static readonly Lazy<IReadOnlyCollection<Group>> Groups = new(Enumerable.Range(0, GroupsCount).Select(e => GroupFaker.Generate()).ToList());

    private static readonly Faker<Student> StudentFaker = new Faker<Student>()
       .RuleFor(e => e.id, r => Guid.NewGuid())
       .RuleFor(e => e.first_name, r => r.Name.FirstName())
       .RuleFor(e => e.last_name, r => r.Name.LastName())
       .RuleFor(e => e.birth_date, r => 
       {
           var birthDate = r.Person.DateOfBirth;
           return new DateOnly(birthDate.Year, birthDate.Month, birthDate.Day);
       })
       .RuleFor(e => e.group_id, r => r.PickRandom(Groups.Value.Select(e => e.id)));
       
    public static readonly Lazy<IReadOnlyCollection<Student>> Students = new(Enumerable.Range(0, StudentsCount).Select(e => StudentFaker.Generate()).ToList());

    private static readonly Faker<Speciality> SpecialityFaker = new Faker<Speciality>()
       .RuleFor(e => e.id, r => Guid.NewGuid())
       .RuleFor(e => e.title, r =>
       {
           var value = $"{r.Company.CompanyName()}_{r.UniqueIndex}";
           int maxLength = Constraints.Speciality.MaxTitleLength;
           return value.Length <= maxLength ? value : value[..maxLength];
       })
       .RuleFor(e => e.code, r => $"{r.Random.Number(1, 99):00}.{r.Random.Number(1, 99):00}.{r.Random.Number(1, 99):00}");

    public static readonly Lazy<IReadOnlyCollection<Speciality>> Specialities = new(Enumerable.Range(0, SpecialitiesCount).Select(e => SpecialityFaker.Generate()).ToList());

    private static readonly Faker<EducationContract> EducationContractFaker = new Faker<EducationContract>()
       .RuleFor(e => e.education_form, r => r.PickRandom(EducationForms.Enumerate()))
       .RuleFor(e => e.admission_date, f => f.Date.RecentDateOnly())
       .RuleFor(e => e.conclusion_date, f => DateOnly.FromDateTime(DateTime.Now))
       .RuleFor(e => e.graduation_date, (f, u) => f.Date.BetweenDateOnly(u.admission_date, f.Date.FutureDateOnly()))
       .RuleFor(e => e.student_id, r => r.PickRandom(Students.Value.Select(e => e.id)))
       .RuleFor(e => e.speciality_id, r => r.PickRandom(Specialities.Value.Select(e => e.id)));

    public static readonly Lazy<IReadOnlyCollection<EducationContract>> EducationContracts = new(GenerateEducationContracts);

    private static List<EducationContract> GenerateEducationContracts()
    {
        var result = new List<EducationContract>();
        foreach (var item in Students.Value)
        {
            var contract = EducationContractFaker.Generate();
            result.Add(new EducationContract
            {
                admission_date = contract.admission_date,
                conclusion_date = contract.conclusion_date,
                student_id = item.id,
                graduation_date = contract.graduation_date,
                education_form = contract.education_form,
                speciality_id = contract.speciality_id
            });
        }

        return result;
    }
}