using Entities;
using ServicesContracts.DTO.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContracts.DTO.PersonDtos
{
    public class UpdatePersonDto:PersonBase
    {
        [Required(ErrorMessage ="The Id should not be blanck")]
        public Guid Id { get; set; }
        public GenderOptions? Gender { get; set; }

        public Person ToPerson()
        {
            return new Person()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                Address = Address,
                CountryId = CountryId,
                DateOfBirth = DateOfBirth,
                ReceiveNewsLetters = ReceiveNewsLetters,
                Gender = Gender.ToString(),
            };

        }
    }
}
