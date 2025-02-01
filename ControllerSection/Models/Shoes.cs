using Microsoft.AspNetCore.Mvc;

namespace ControllerSection.Models
{
    public class Shoes
    {
        //[FromQuery]
        public int? ShoesId { get; set; }
        public string? Name { get; set; }
        public override string ToString()
        {
            return $"The Shoes Id is => {ShoesId} And The Name Is => {Name} ";
        }
    }
}
