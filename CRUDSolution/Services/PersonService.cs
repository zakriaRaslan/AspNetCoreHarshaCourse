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
        private readonly List<Person> _people;
        private readonly ICountryService _countryService;
        public PersonService(bool initialize = true)
        {
            this._people = new List<Person>();
            _countryService = new CountryServices();
            if (initialize)
            {
                _people.Add(new Person() { Id = Guid.Parse("8082ED0C-396D-4162-AD1D-29A13F929824"), Name = "Aguste", Email = "aleddy0@booking.com", DateOfBirth = DateTime.Parse("1993-01-02"), Gender = "Male", Address = "0858 Novick Terrace", ReceiveNewsLetters = false, CountryId = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B") });

                _people.Add(new Person() { Id = Guid.Parse("06D15BAD-52F4-498E-B478-ACAD847ABFAA"), Name = "Jasmina", Email = "jsyddie1@miibeian.gov.cn", DateOfBirth = DateTime.Parse("1991-06-24"), Gender = "Female", Address = "0742 Fieldstone Lane", ReceiveNewsLetters = true, CountryId = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F") });

                _people.Add(new Person() { Id = Guid.Parse("D3EA677A-0F5B-41EA-8FEF-EA2FC41900FD"), Name = "Kendall", Email = "khaquard2@arstechnica.com", DateOfBirth = DateTime.Parse("1993-08-13"), Gender = "Male", Address = "7050 Pawling Alley", ReceiveNewsLetters = false, CountryId = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F") });

                _people.Add(new Person() { Id = Guid.Parse("89452EDB-BF8C-4283-9BA4-8259FD4A7A76"), Name = "Kilian", Email = "kaizikowitz3@joomla.org", DateOfBirth = DateTime.Parse("1991-06-17"), Gender = "Male", Address = "233 Buhler Junction", ReceiveNewsLetters = true, CountryId = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E") });

                _people.Add(new Person() { Id = Guid.Parse("F5BD5979-1DC1-432C-B1F1-DB5BCCB0E56D"), Name = "Dulcinea", Email = "dbus4@pbs.org", DateOfBirth = DateTime.Parse("1996-09-02"), Gender = "Female", Address = "56 Sundown Point", ReceiveNewsLetters = false, CountryId = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E") });

                _people.Add(new Person() { Id = Guid.Parse("A795E22D-FAED-42F0-B134-F3B89B8683E5"), Name = "Corabelle", Email = "cadams5@t-online.de", DateOfBirth = DateTime.Parse("1993-10-23"), Gender = "Female", Address = "4489 Hazelcrest Place", ReceiveNewsLetters = false, CountryId = Guid.Parse("15889048-AF93-412C-B8F3-22103E943A6D") });

                _people.Add(new Person() { Id = Guid.Parse("3C12D8E8-3C1C-4F57-B6A4-C8CAAC893D7A"), Name = "Faydra", Email = "fbischof6@boston.com", DateOfBirth = DateTime.Parse("1996-02-14"), Gender = "Female", Address = "2010 Farragut Pass", ReceiveNewsLetters = true, CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });

                _people.Add(new Person() { Id = Guid.Parse("7B75097B-BFF2-459F-8EA8-63742BBD7AFB"), Name = "Oby", Email = "oclutheram7@foxnews.com", DateOfBirth = DateTime.Parse("1992-05-31"), Gender = "Male", Address = "2 Fallview Plaza", ReceiveNewsLetters = false, CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });

                _people.Add(new Person() { Id = Guid.Parse("6717C42D-16EC-4F15-80D8-4C7413E250CB"), Name = "Seumas", Email = "ssimonitto8@biglobe.ne.jp", DateOfBirth = DateTime.Parse("1999-02-02"), Gender = "Male", Address = "76779 Norway Maple Crossing", ReceiveNewsLetters = false, CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });

                _people.Add(new Person() { Id = Guid.Parse("6E789C86-C8A6-4F18-821C-2ABDB2E95982"), Name = "Freemon", Email = "faugustin9@vimeo.com", DateOfBirth = DateTime.Parse("1996-04-27"), Gender = "Male", Address = "8754 Becker Street", ReceiveNewsLetters = false, CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });

            }
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

            _people.Add(person);

            return ConvertPersonToPersonResponse(person);

        }
        public PersonResponse? GetById(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            return _people.FirstOrDefault(p => p.Id == id)?.ToPersonResponse();
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _people.Select(person => person.ToPersonResponse()).ToList();
        }

        public List<PersonResponse> GetFilterdPersons(string searchBy, string? searchFor)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> matchingPersons = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchFor))
                return matchingPersons;

            switch (searchBy)
            {
                case nameof(PersonResponse.Name):
                    matchingPersons = allPersons.Where(person =>
                    (!string.IsNullOrEmpty(person.Name) ?
                    person.Name.Contains(searchFor, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(PersonResponse.Email):
                    matchingPersons = allPersons.Where(person =>
                    (!string.IsNullOrEmpty(person.Email) ?
                    person.Email.Contains(searchFor, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;


                case nameof(PersonResponse.DateOfBirth):
                    matchingPersons = allPersons.Where(person =>
                    (person.DateOfBirth != null) ?
                    person.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchFor, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(PersonResponse.Gender):
                    matchingPersons = allPersons.Where(person =>
                    (!string.IsNullOrEmpty(person.Gender) ?
                    person.Gender.Contains(searchFor, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(PersonResponse.Id):
                    matchingPersons = allPersons.Where(person =>
                    (!string.IsNullOrEmpty(person.Country) ?
                    person.Country.Contains(searchFor, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(PersonResponse.Address):
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

            Person? person_from_list = _people.FirstOrDefault(person => person.Id == personToUpdate.Id);
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

            Person? person = _people.Find(person => person.Id == id);
            if (person == null) return false;

            _people.RemoveAll(person => person.Id == id);
            return true;
        }
    }

}
