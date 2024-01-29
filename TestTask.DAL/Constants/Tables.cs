namespace TestTask.DAL.Constants;

public static class Tables
{
    public const string Departaments = "departaments";
    public const string Groups = "groups";
    public const string Students = "students";
    public const string EducationContracts = "education_contracts";
    public const string Specialities = "specialities";

    public static IEnumerable<string> Enumerate()
    {
        yield return Departaments;
        yield return Groups;
        yield return Students;
        yield return EducationContracts;
        yield return Specialities;
    }
}