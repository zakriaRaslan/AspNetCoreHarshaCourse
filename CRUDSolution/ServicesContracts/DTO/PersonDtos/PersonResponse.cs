using Entities;
using ServicesContracts.DTO.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContracts.DTO.PersonDtos
{
    public class PersonResponse:PersonBase
    {
        public Guid Id { get; set; }
        public string? Gender { get; set; }
        public string? Country { get; set; }
        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if(obj.GetType() != typeof(PersonResponse))
                return false;
            PersonResponse PersonToCompare = (PersonResponse) obj;

            return this.Id == PersonToCompare.Id;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Id: {Id} , Name: {Name} , Address: {Address}, Gender: {Gender} , Age: {Age} , BirthDate: {DateOfBirth.ToString()} ,Country: {Country}";
        }



        public  UpdatePersonDto ToUpdatePersonDto()
        {
            return new UpdatePersonDto()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                Address = Address,
                CountryId = CountryId,
                DateOfBirth = DateOfBirth,
                ReceiveNewsLetters = ReceiveNewsLetters,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),

            };
        }
    }
}
