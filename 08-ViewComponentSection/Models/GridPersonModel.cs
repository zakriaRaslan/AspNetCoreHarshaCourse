namespace _08_ViewComponentSection.Models
{
    public class GridPersonModel
    {
        public string GridTitle { get; set; } =string.Empty;
        public List<Person> Persons { get; set;} = new List<Person>();
    }
}
