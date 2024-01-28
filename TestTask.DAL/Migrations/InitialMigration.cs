using FluentMigrator;
using TestTask.DAL.Constants;
using TestTask.DAL.Extensions;
using TestTask.DAL.Models;

namespace TestTask.DAL.Migrations;

[Migration(1, description: "Initial migration that creates all required tables, constraints and indexes.")]
public class InitialMigration : Migration
{
    public override void Down()
    {
        #region --Indexes--

        Delete.Index()
            .OnTable(Tables.Groups)
            .OnColumn(nameof(Group.departament_id));

        Delete.Index()
            .OnTable(Tables.Students)
            .OnColumn(nameof(Student.group_id));

        Delete.Index()
            .OnTable(Tables.EducationContracts)
            .OnColumn(nameof(EducationContract.student_id));

        Delete.Index()
            .OnTable(Tables.EducationContracts)
            .OnColumn(nameof(EducationContract.speciality_id));

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
            .WithColumn(nameof(Departament.title)).AsString(Constraints.Departament.MaxTitleLength).NotNullable().Unique()
            .WithColumn(nameof(Departament.description)).AsString(Constraints.Departament.MaxDescriptionLength).NotNullable();

        Create.Table(Tables.Groups)
            .WithIdColumn()
            .WithColumn(nameof(Group.title)).AsString(Constraints.Group.MaxTitleLength).NotNullable().Unique()
            .WithColumn(nameof(Group.departament_id)).AsGuid().NotNullable().ForeignKey(Tables.Departaments, "id");

        Create.Table(Tables.Specialities)
            .WithIdColumn()
            .WithColumn(nameof(Speciality.title)).AsString(Constraints.Speciality.MaxTitleLength).NotNullable().Unique()
            .WithColumn(nameof(Speciality.code)).AsString(Constraints.Speciality.MaxCodeLength).NotNullable().Unique();

        Create.Table(Tables.Students)
            .WithIdColumn()
            .WithColumn(nameof(Student.first_name)).AsString(Constraints.Student.MaxNameLength).NotNullable()
            .WithColumn(nameof(Student.last_name)).AsString(Constraints.Student.MaxNameLength).NotNullable()
            .WithColumn(nameof(Student.birth_date)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(Student.group_id)).AsGuid().ForeignKey(Tables.Groups, "id");

        Create.Table(Tables.EducationContracts)
            .WithIdColumn()
            .WithColumn(nameof(EducationContract.admission_date)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(EducationContract.graduation_date)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(EducationContract.conclusion_date)).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(EducationContract.education_form)).AsString().NotNullable()
            .WithColumn(nameof(EducationContract.student_id)).AsGuid().ForeignKey(Tables.Students, "id")
            .WithColumn(nameof(EducationContract.speciality_id)).AsGuid().ForeignKey(Tables.Specialities, "id");

        #endregion

        #region --Indexes--

        Create.Index()
            .OnTable(Tables.Groups)
            .OnColumn(nameof(Group.departament_id))
            .Ascending()
            .WithOptions()
            .NonClustered();

        Create.Index()
            .OnTable(Tables.Students)
            .OnColumn(nameof(Student.group_id))
            .Ascending()
            .WithOptions()
            .NonClustered();

        Create.Index()
            .OnTable(Tables.EducationContracts)
            .OnColumn(nameof(EducationContract.student_id))
            .Ascending()
            .WithOptions()
            .NonClustered();

        Create.Index()
            .OnTable(Tables.EducationContracts)
            .OnColumn(nameof(EducationContract.speciality_id))
            .Ascending()
            .WithOptions()
            .NonClustered();

        #endregion
    }
}