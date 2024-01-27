﻿using Bogus;
using TestTask.DAL.Models;
using TestTask.Domain.Constants;
using TestTask.Domain.Enums;

namespace TestTask.DAL;

internal static class Data
{
    private const int DEPARTAMENTS_COUNT = 15;

    private const int GROUPS_COUNT = 30;

    private const int STUDENTS_COUNT = 300;

    private const int SPECIALITIES_COUNT = 50;

    private const int EDUCATION_CONTRACTS_COUNT = STUDENTS_COUNT;

    private static readonly Faker<DepartamentDataModel> departamentFaker = new Faker<DepartamentDataModel>()
       .RuleFor(e => e.id, r => Guid.NewGuid())
       .RuleFor(e => e.title, r => 
       {
           var value = $"{r.Company.CompanyName()}_{r.UniqueIndex}";
           int maxLength = Constraints.Departament.MaxTitleLength;
           return value.Length <= maxLength? value : value[..maxLength]; 
       })
       .RuleFor(e => e.description, r =>
       {
           var value = r.Lorem.Paragraph();
           int maxLength = Constraints.Departament.MaxDescriptionLength;
           return value.Length <= maxLength ? value : value[..maxLength];
       });

    public static readonly Lazy<IReadOnlyCollection<DepartamentDataModel>> Departaments = new(Enumerable.Range(0, DEPARTAMENTS_COUNT).Select(e => departamentFaker.Generate()).ToList());

    private static readonly Faker<GroupDataModel> groupFaker = new Faker<GroupDataModel>()
       .RuleFor(e => e.id, r => Guid.NewGuid())
       .RuleFor(e => e.title, r =>
       {
           var value = $"{r.Company.CompanyName()}_{r.UniqueIndex}";
           int maxLength = Constraints.Group.MaxTitleLength;
           return value.Length <= maxLength ? value : value[..maxLength];
       })
       .RuleFor(e => e.departament_id, r => r.PickRandom(Departaments.Value.Select(e => e.id)));

    public static readonly Lazy<IReadOnlyCollection<GroupDataModel>> Groups = new(Enumerable.Range(0, GROUPS_COUNT).Select(e => groupFaker.Generate()).ToList());

    private static readonly Faker<StudentDataModel> studentFaker = new Faker<StudentDataModel>()
       .RuleFor(e => e.id, r => Guid.NewGuid())
       .RuleFor(e => e.first_name, r => r.Name.FirstName())
       .RuleFor(e => e.last_name, r => r.Name.LastName())
       .RuleFor(e => e.birth_date, r => r.Person.DateOfBirth)
       .RuleFor(e => e.group_id, r => r.PickRandom(Groups.Value.Select(e => e.id)));
       
    public static readonly Lazy<IReadOnlyCollection<StudentDataModel>> Students = new(Enumerable.Range(0, STUDENTS_COUNT).Select(e => studentFaker.Generate()).ToList());

    private static readonly Faker<SpecialityDataModel> specialityFaker = new Faker<SpecialityDataModel>()
       .RuleFor(e => e.id, r => Guid.NewGuid())
       .RuleFor(e => e.title, r =>
       {
           var value = $"{r.Company.CompanyName()}_{r.UniqueIndex}";
           int maxLength = Constraints.Speciality.MaxTitleLength;
           return value.Length <= maxLength ? value : value[..maxLength];
       })
       .RuleFor(e => e.code, r => $"{r.Random.Number(1, 99):00}.{r.Random.Number(1, 99):00}.{r.Random.Number(1, 99):00}");

    public static readonly Lazy<IReadOnlyCollection<SpecialityDataModel>> Specialities = new(Enumerable.Range(0, SPECIALITIES_COUNT).Select(e => specialityFaker.Generate()).ToList());

    private static readonly Faker<EducationContractDataModel> educationContractFaker = new Faker<EducationContractDataModel>()
       .RuleFor(e => e.id, r => Guid.NewGuid())
       .RuleFor(e => e.education_form, r => r.PickRandom(Enum.GetNames<EducationForms>()))
       .RuleFor(e => e.admission_date, f => f.Date.RecentOffset())
       .RuleFor(e => e.conclusion_date, f => DateTimeOffset.UtcNow)
       .RuleFor(e => e.graduation_date, (f, u) => f.Date.BetweenOffset(u.admission_date, f.Date.FutureOffset()))
       .RuleFor(e => e.student_id, r => r.PickRandom(Students.Value.Select(e => e.id)))
       .RuleFor(e => e.speciality_id, r => r.PickRandom(Specialities.Value.Select(e => e.id)));

    public static readonly Lazy<IReadOnlyCollection<EducationContractDataModel>> EducationContracts = new(Enumerable.Range(0, EDUCATION_CONTRACTS_COUNT).Select(e => educationContractFaker.Generate()).ToList());
}