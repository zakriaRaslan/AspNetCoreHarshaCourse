using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(40)]
        public string? Name { get; set; }
        [StringLength(40)]
        public string? Email { get; set; }
        [StringLength(200)]
        public string? Address { get; set; }
        [StringLength(10)]
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public Guid? CountryId { get; set; }
    }
}
