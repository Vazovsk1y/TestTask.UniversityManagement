namespace TestTask.DAL.Constants;

public static class Constraints
{
    public static class Departament
    {
        public const int MaxTitleLength = 75;

        public const int MaxDescriptionLength = 150;
    }

    public static class Group
    {
        public const int MaxTitleLength = 25;
    }

    public static class Speciality
    {
        public const int MaxTitleLength = 100;

        public const int MaxCodeLength = 15;
    }

    public static class Student
    {
        public const int MaxNameLength = 50;
    }
}
