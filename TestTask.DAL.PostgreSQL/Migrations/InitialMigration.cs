using FluentMigrator;
using System.Data;
using TestTask.DAL.PostgreSQL.Extensions;
using TestTask.DAL.PostgreSQL.Constants;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.DAL.PostgreSQL.Migrations;

[Migration(1, description: "Initial migration that creates all required tables, constraints and indexes.")]
public class InitialMigration : Migration
{
    public override void Down()
    {
        #region --Indexes--

        Delete.Index()
            .OnTable(Tables.EducationContracts)
            .OnColumn(nameof(EducationContract.student_id));

        Delete.Index()
            .OnTable(Tables.EducationContracts)
            .OnColumn(nameof(EducationContract.speciality_id));

        Delete.Index()
            .OnTable(Tables.Students)
            .OnColumn(nameof(Student.group_id));

        Delete.Index()
            .OnTable(Tables.Students)
            .OnColumn(nameof(Student.id));

        Delete.Index()
            .OnTable(Tables.Specialities)
            .OnColumn(nameof(Speciality.id));

        Delete.Index()
            .OnTable(Tables.Groups)
            .OnColumn(nameof(Group.departament_id));

        Delete.Index()
            .OnTable(Tables.Groups)
            .OnColumn(nameof(Group.id));

        Delete.Index()
            .OnTable(Tables.Departments)
            .OnColumn(nameof(Department.id));

        #endregion

        #region --Tables--

        Delete.Table(Tables.EducationContracts);
        Delete.Table(Tables.Students);
        Delete.Table(Tables.Specialities);
        Delete.Table(Tables.Groups);
        Delete.Table(Tables.Departments);

        #endregion
    }

    public override void Up()
    {
        #region --Tables--

        Create.Table(Tables.Departments)
            .WithPrimaryKeyColumn()
            .WithColumn(nameof(Department.title)).AsString(Constraints.Department.MaxTitleLength).NotNullable().Unique()
            .WithColumn(nameof(Department.description)).AsString(Constraints.Department.MaxDescriptionLength).NotNullable();

        Create.Table(Tables.Groups)
            .WithPrimaryKeyColumn()
            .WithColumn(nameof(Group.title)).AsString(Constraints.Group.MaxTitleLength).NotNullable().Unique()
            .WithColumn(nameof(Group.departament_id)).AsGuid().NotNullable().ForeignKey(Tables.Departments, "id");

        Create.Table(Tables.Specialities)
            .WithPrimaryKeyColumn()
            .WithColumn(nameof(Speciality.title)).AsString(Constraints.Speciality.MaxTitleLength).NotNullable().Unique()
            .WithColumn(nameof(Speciality.code)).AsString(Constraints.Speciality.MaxCodeLength).NotNullable().Unique();

        Create.Table(Tables.Students)
            .WithPrimaryKeyColumn()
            .WithColumn(nameof(Student.first_name)).AsString(Constraints.Student.MaxNameLength).NotNullable()
            .WithColumn(nameof(Student.last_name)).AsString(Constraints.Student.MaxNameLength).NotNullable()
            .WithColumn(nameof(Student.birth_date)).AsDate().NotNullable()
            .WithColumn(nameof(Student.group_id)).AsGuid().ForeignKey(Tables.Groups, "id").OnDelete(Rule.Cascade);

        Create.Table(Tables.EducationContracts)
            .WithColumn(nameof(EducationContract.student_id)).AsGuid().ForeignKey(Tables.Students, "id").OnDelete(Rule.Cascade).PrimaryKey()
            .WithColumn(nameof(EducationContract.speciality_id)).AsGuid().ForeignKey(Tables.Specialities, "id")
            .WithColumn(nameof(EducationContract.admission_date)).AsDate().NotNullable()
            .WithColumn(nameof(EducationContract.graduation_date)).AsDate().NotNullable()
            .WithColumn(nameof(EducationContract.conclusion_date)).AsDate().NotNullable()
            .WithColumn(nameof(EducationContract.education_form)).AsString().NotNullable();

        #endregion

        #region --Indexes--

        Create.Index()
            .OnTable(Tables.Departments)
            .OnColumn(nameof(Department.id))
            .Ascending()
            .WithOptions()
            .Clustered();

        Create.Index()
            .OnTable(Tables.Groups)
            .OnColumn(nameof(Group.id))
            .Ascending()
            .WithOptions()
            .Clustered();

        Create.Index()
            .OnTable(Tables.Groups)
            .OnColumn(nameof(Group.departament_id))
            .Ascending()
            .WithOptions()
            .NonClustered();

        Create.Index()
            .OnTable(Tables.Specialities)
            .OnColumn(nameof(Speciality.id))
            .Ascending()
            .WithOptions()
            .Clustered();

        Create.Index()
            .OnTable(Tables.Students)
            .OnColumn(nameof(Student.id))
            .Ascending()
            .WithOptions()
            .Clustered();

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
            .Clustered();

        Create.Index()
            .OnTable(Tables.EducationContracts)
            .OnColumn(nameof(EducationContract.speciality_id))
            .Ascending()
            .WithOptions()
            .NonClustered();

        #endregion
    }
}