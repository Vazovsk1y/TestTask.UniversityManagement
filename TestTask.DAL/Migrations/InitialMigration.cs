using FluentMigrator;
using TestTask.DAL.Constants;
using TestTask.DAL.Extensions;
using TestTask.DAL.Models;
using TestTask.Domain.Constants;

namespace TestTask.DAL.Migrations;

[Migration(1, description: "Initial migration that creates all required tables, constraints and indexes.")]
public class InitialMigration : Migration
{
    public override void Down()
    {
        #region --Indexes--

        Delete.Index()
            .OnTable(Tables.Groups)
            .OnColumn(nameof(GroupDataModel.departament_id));

        Delete.Index()
            .OnTable(Tables.Students)
            .OnColumn(nameof(StudentDataModel.group_id));

        Delete.Index()
            .OnTable(Tables.EducationContracts)
            .OnColumn(nameof(EducationContractDataModel.student_id));

        Delete.Index()
            .OnTable(Tables.EducationContracts)
            .OnColumn(nameof(EducationContractDataModel.speciality_id));

        #endregion

        #region --Tables--

        Delete.Table(Tables.EducationContracts);
        Delete.Table(Tables.Students);
        Delete.Table(Tables.Specialities);
        Delete.Table(Tables.Groups);
        Delete.Table(Tables.Departaments);
        Delete.Table("VersionInfo");

        #endregion
    }

    public override void Up()
    {
        #region --Tables--

        Create.Table(Tables.Departaments)
            .WithIdColumn()
            .WithColumn(nameof(DepartamentDataModel.title)).AsString(Constraints.Departament.MaxTitleLength).NotNullable().Unique()
            .WithColumn(nameof(DepartamentDataModel.description)).AsString(Constraints.Departament.MaxDescriptionLength).NotNullable();

        Create.Table(Tables.Groups)
            .WithIdColumn()
            .WithColumn(nameof(GroupDataModel.title)).AsString(Constraints.Group.MaxTitleLength).NotNullable().Unique()
            .WithColumn(nameof(GroupDataModel.departament_id)).AsGuid().NotNullable().ForeignKey(Tables.Departaments, "id");

        Create.Table(Tables.Specialities)
            .WithIdColumn()
            .WithColumn(nameof(SpecialityDataModel.title)).AsString(Constraints.Speciality.MaxTitleLength).NotNullable().Unique()
            .WithColumn(nameof(SpecialityDataModel.code)).AsString(Constraints.Speciality.MaxCodeLength).NotNullable().Unique();

        Create.Table(Tables.Students)
            .WithIdColumn()
            .WithColumn(nameof(StudentDataModel.first_name)).AsString(Constraints.Student.MaxNameLength).NotNullable()
            .WithColumn(nameof(StudentDataModel.last_name)).AsString(Constraints.Student.MaxNameLength).NotNullable()
            .WithColumn(nameof(StudentDataModel.birth_date)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(StudentDataModel.group_id)).AsGuid().ForeignKey(Tables.Groups, "id");

        Create.Table(Tables.EducationContracts)
            .WithIdColumn()
            .WithColumn(nameof(EducationContractDataModel.admission_date)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(EducationContractDataModel.graduation_date)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(EducationContractDataModel.conclusion_date)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(EducationContractDataModel.education_form)).AsString().NotNullable()
            .WithColumn(nameof(EducationContractDataModel.student_id)).AsGuid().ForeignKey(Tables.Students, "id")
            .WithColumn(nameof(EducationContractDataModel.speciality_id)).AsGuid().ForeignKey(Tables.Specialities, "id");

        #endregion

        #region --Indexes--

        Create.Index()
            .OnTable(Tables.Groups)
            .OnColumn(nameof(GroupDataModel.departament_id))
            .Ascending()
            .WithOptions()
            .NonClustered();

        Create.Index()
            .OnTable(Tables.Students)
            .OnColumn(nameof(StudentDataModel.group_id))
            .Ascending()
            .WithOptions()
            .NonClustered();

        Create.Index()
            .OnTable(Tables.EducationContracts)
            .OnColumn(nameof(EducationContractDataModel.student_id))
            .Ascending()
            .WithOptions()
            .NonClustered();

        Create.Index()
            .OnTable(Tables.EducationContracts)
            .OnColumn(nameof(EducationContractDataModel.speciality_id))
            .Ascending()
            .WithOptions()
            .NonClustered();

        #endregion
    }
}