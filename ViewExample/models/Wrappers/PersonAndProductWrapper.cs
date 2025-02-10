namespace ViewExample.models.Wrappers
{
    public class PersonAndProductWrapper
    {
        public IEnumerable<Product>? ProductsDate { get; set; } 
        public IEnumerable<Person>? PersonsData { get; set; } 
    }
}
