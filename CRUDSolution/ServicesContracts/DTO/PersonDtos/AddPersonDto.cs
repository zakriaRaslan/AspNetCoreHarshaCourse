using Entities;
using ServicesContracts.DTO.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContracts.DTO.PersonDtos
{
    public class AddPersonDto:PersonBase
    {

        public GenderOptions? Gender { get; set; }

        public Person ToPerson()
        {
            return new Person()
            {
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
