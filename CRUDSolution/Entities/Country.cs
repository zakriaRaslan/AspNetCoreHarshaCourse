using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    ///  This is the domain class for Country
    /// </summary>
    public class Country
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(40)]
        public string? Name { get; set; }
    }
}
