using Entities;
using ServicesContracts.DTO.Helpers;

namespace ServicesContracts.DTO.PersonDtos
{
    public static class PersonExtensions
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                Id = person.Id,
                Name = person.Name,
                Gender = person.Gender,
                Address = person.Address,
                CountryId = person.CountryId,
                DateOfBirth = person.DateOfBirth,
                Email = person.Email,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Age =(person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25):null,
                
                
            };
        }

      
    }
}
