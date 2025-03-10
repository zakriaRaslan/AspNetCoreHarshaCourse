using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContracts.DTO.PersonDtos
{
    public class PersonBase
    {
        [Required(ErrorMessage ="The name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "The email must be valid")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required(ErrorMessage ="The Address is required")]
        public string? Address { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="The date is required")]
        public DateTime? DateOfBirth { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public Guid? CountryId { get; set; }
    }
}
