namespace ViewExample.models
{
    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }

        public Gender PersonGender { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
