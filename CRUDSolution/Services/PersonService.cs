using Entities;
using Services.Helpers;
using ServicesContracts;
using ServicesContracts.DTO.Helpers;
using ServicesContracts.DTO.PersonDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PersonService : IPersonService
    {
        private readonly List<Person> people;
        private readonly ICountryService _countryService;
        public PersonService()
        {
            this.people = new List<Person>();
            _countryService = new CountryServices();
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countryService.GetById(personResponse.CountryId)?.Name;
            return personResponse;
        }


        public PersonResponse AddPerson(AddPersonDto? personToAdd)
        {
            if (personToAdd == null) throw new ArgumentNullException(nameof(personToAdd));

            ValidationHelper.ModelValidation(personToAdd);


            Person person = personToAdd.ToPerson();

            person.Id = Guid.NewGuid();

            people.Add(person);

            return ConvertPersonToPersonResponse(person);

        }
        public PersonResponse? GetById(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            return people.FirstOrDefault(p => p.Id == id)?.ToPersonResponse();
        }

        public List<PersonResponse> GetAllPersons()
        {
            return people.Select(person => person.ToPersonResponse()).ToList();
        }

        public List<PersonResponse> GetFilterdPersons(string searchBy, string? searchFor)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> matchingPersons = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchFor))
                return matchingPersons;

            switch (searchBy)
            {
                case nameof(Person.Name):
                    matchingPersons = allPersons.Where(person =>
                    (!string.IsNullOrEmpty(person.Name) ?
                    person.Name.Contains(searchFor, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Email):
                    matchingPersons = allPersons.Where(person =>
                    (!string.IsNullOrEmpty(person.Email) ?
                    person.Email.Contains(searchFor, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;


                case nameof(Person.DateOfBirth):
                    matchingPersons = allPersons.Where(person =>
                    (person.DateOfBirth != null) ?
                    person.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchFor, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Gender):
                    matchingPersons = allPersons.Where(person =>
                    (!string.IsNullOrEmpty(person.Gender) ?
                    person.Gender.Contains(searchFor, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Id):
                    matchingPersons = allPersons.Where(person =>
                    (!string.IsNullOrEmpty(person.Country) ?
                    person.Country.Contains(searchFor, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Address):
                    matchingPersons = allPersons.Where(person =>
                    (!string.IsNullOrEmpty(person.Address) ?
                    person.Address.Contains(searchFor, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                default: matchingPersons = allPersons; break;
            }
            return matchingPersons;
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortedKey, SortOrderOption sortOption)
        {
            if (string.IsNullOrEmpty(sortedKey)) return allPersons;

            List<PersonResponse> sortedPersons = (sortedKey, sortOption) switch
            {
                (nameof(PersonResponse.Name), SortOrderOption.ASC) => allPersons.OrderBy(person => person.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Name), SortOrderOption.DESC) => allPersons.OrderByDescending(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOption.ASC) => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOption.DESC) => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOption.ASC) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOption.DESC) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.Age), SortOrderOption.ASC) => allPersons.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOption.DESC) => allPersons.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOption.ASC) => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOption.DESC) => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOption.ASC) => allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOption.DESC) => allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOption.ASC) => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOption.DESC) => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOption.ASC) => allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOption.DESC) => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

                _ => allPersons
            };

            return sortedPersons;
        }

        public PersonResponse UpdatePerson(UpdatePersonDto? personToUpdate)
        {
            if (personToUpdate == null) throw new ArgumentNullException(nameof(personToUpdate));

            ValidationHelper.ModelValidation(personToUpdate);

            Person? person_from_list = people.FirstOrDefault(person => person.Id == personToUpdate.Id);
            if (person_from_list == null)
                throw new ArgumentException("This User Does not exist");

            //update all details
            person_from_list.Name = personToUpdate.Name;
            person_from_list.Email = personToUpdate.Email;
            person_from_list.DateOfBirth = personToUpdate.DateOfBirth;
            person_from_list.Gender = personToUpdate.Gender.ToString();
            person_from_list.CountryId = personToUpdate.CountryId;
            person_from_list.Address = personToUpdate.Address;
            person_from_list.ReceiveNewsLetters = personToUpdate.ReceiveNewsLetters;

            return person_from_list.ToPersonResponse();
        }

        public bool DeleteById(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            Person? person = people.Find(person => person.Id == id);
            if (person == null) return false;

            people.RemoveAll(person => person.Id == id);
            return true;
        }
    }

}
