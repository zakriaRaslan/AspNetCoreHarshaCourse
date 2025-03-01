using Services;
using ServicesContracts;
using ServicesContracts.DTO.CountryDTOs;
using ServicesContracts.DTO.Helpers;
using ServicesContracts.DTO.PersonDtos;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class PersonServicesTests
    {
        private readonly IPersonService _personService;
        private readonly ICountryService _countryService;
        private readonly ITestOutputHelper _outputHelper;
        public PersonServicesTests(ITestOutputHelper outputHelper)
        {
            _personService = new PersonService();
            _countryService = new CountryServices();
            _outputHelper = outputHelper;
        }


        #region AddPerson Tests
        [Fact]
        public void AddPerson_NullAddPersonParam()
        {
            //Arrang
            AddPersonDto? nullPersonToAdd = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.AddPerson(nullPersonToAdd);
            });

        }


        [Fact]
        public void AddPerson_NullNamePropInPerson()
        {
            AddPersonDto addPersonWithNullName = new AddPersonDto()
            {
                Name = null,
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.AddPerson(addPersonWithNullName);
            });
        }

        [Fact]
        public void AddPerson_SuccessCase()
        {
            //Arange
            AddPersonDto addPersonDto = new AddPersonDto()
            {
                Name = "name",
                Address = "Address",
                CountryId = Guid.NewGuid(),
                DateOfBirth = DateTime.Parse("2000-10-10"),
                Email = "Person@host.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true
            };

            PersonResponse PersonFromAddPersonMethod = _personService.AddPerson(addPersonDto);
            List<PersonResponse> PersonList = _personService.GetAllPersons();

            Assert.NotNull(PersonFromAddPersonMethod);
            Assert.Contains(PersonFromAddPersonMethod, PersonList);

        }

        #endregion

        #region GetById Tests
        [Fact]
        public void GetById_NullIdCase()
        {
            //Arrang
            Guid? nullId = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.GetById(nullId);
            });
        }

        [Fact]
        public void GetById_SuccessCase()
        {
            AddCountryRequest addCountryRequest = new AddCountryRequest()
            {
                Name = "Egypr"
            };
            CountryResponse countryResponseFromAddCountry = _countryService.Add(addCountryRequest);

            AddPersonDto addPersonDto = new AddPersonDto()
            {
                Name = "name",
                Address = "Address",
                CountryId = countryResponseFromAddCountry.Id,
                DateOfBirth = DateTime.Parse("2000-10-10"),
                Email = "Person@host.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true
            };
            //Act
            PersonResponse personResponseFromAddperson = _personService.AddPerson(addPersonDto);

            PersonResponse? personResponseFromGetById = _personService.GetById(personResponseFromAddperson.Id);


            //Assert
            Assert.Equal(personResponseFromAddperson, personResponseFromGetById);
        }


        #endregion


        #region GetAllPersons Tests

        [Fact]
        public void GetAllPersons_EmptyList()
        {
            //Act
            List<PersonResponse> persons_from_get = _personService.GetAllPersons();

            //Assert
            Assert.Empty(persons_from_get);
        }

        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            //Arrange

            List<PersonResponse> person_response_list_from_add = AddDumyPersons();

            // Print The Expected List By Using ITestOutputHelper
            _outputHelper.WriteLine("Expected List:");
            foreach (PersonResponse person in person_response_list_from_add)
            {
                _outputHelper.WriteLine(person.ToString());
            }

            //Act
            List<PersonResponse> persons_list_from_get = _personService.GetAllPersons();

            // Print The Actual List By Using ITestOutputHelper
            _outputHelper.WriteLine("Actual List:");
            foreach (PersonResponse person in persons_list_from_get)
            {
                _outputHelper.WriteLine(person.ToString());
            }



            //Assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, persons_list_from_get);
            }
        }

        #endregion


        #region GetFilteredPersons Tests

        // in the case of search for the empty name it should return all the list
        [Fact]
        public void GetfilteredPerosons_WithEmptyName()
        {

            List<PersonResponse> person_response_list_from_add = AddDumyPersons();

            // Print The Expected List By Using ITestOutputHelper
            _outputHelper.WriteLine("Expected List:");
            foreach (PersonResponse person in person_response_list_from_add)
            {
                _outputHelper.WriteLine(person.ToString());
            }

            //Act
            List<PersonResponse> persons_list_from_getFilterd = _personService.GetFilterdPersons(nameof(PersonResponse.Name), "");


            // Print The Actual List By Using ITestOutputHelper
            _outputHelper.WriteLine("Actual List:");
            foreach (PersonResponse person in persons_list_from_getFilterd)
            {
                _outputHelper.WriteLine(person.ToString());
            }



            //Assert
            foreach (PersonResponse person in person_response_list_from_add)
            {
                Assert.Contains(person, persons_list_from_getFilterd);
            }
        }



        [Fact]
        public void GetfilteredPerosons_WithName()
        {

            List<PersonResponse> person_response_list_from_add = AddDumyPersons();

            // Print The Expected List By Using ITestOutputHelper
            _outputHelper.WriteLine("Expected List:");
            foreach (PersonResponse person in person_response_list_from_add)
            {
                _outputHelper.WriteLine(person.ToString());
            }

            //Act
            List<PersonResponse> persons_list_from_getFilterd = _personService.GetFilterdPersons(nameof(PersonResponse.Name), "ma");


            // Print The Actual List By Using ITestOutputHelper
            _outputHelper.WriteLine("Actual List:");
            foreach (PersonResponse person in persons_list_from_getFilterd)
            {
                _outputHelper.WriteLine(person.ToString());
            }



            //Assert
            foreach (PersonResponse person in person_response_list_from_add)
            {
                if (person.Name != null)
                {
                    if (person.Name.Contains("ma", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(person, persons_list_from_getFilterd);
                    }

                }

            }
        }


        #endregion


        #region GetSortedPersons Tests

        [Fact]
        public void GetSortedPersons_SortByNameCase()
        {
            List<PersonResponse> person_list_from_add_method = AddDumyPersons();

            List<PersonResponse> person_response_list_from_get = _personService.GetAllPersons();

            List<PersonResponse> expected_sorted_list = person_response_list_from_get.OrderByDescending(person => person.Name).ToList();

            _outputHelper.WriteLine("Expected Sorted List");
            foreach (PersonResponse person in expected_sorted_list) { _outputHelper.WriteLine(person.ToString()); }

            List<PersonResponse> person_response_list_from_sorted = _personService
                .GetSortedPersons(person_response_list_from_get, nameof(PersonResponse.Name), SortOrderOption.DESC);
            _outputHelper.WriteLine("Actual Sorted List");
            foreach (PersonResponse person in person_response_list_from_sorted) { _outputHelper.WriteLine(person.ToString()); }

            for (int i = 0; i < expected_sorted_list.Count; i++)
            {
                Assert.Equal(expected_sorted_list[i], person_response_list_from_sorted[i]);
            }


        }

        #endregion

        #region UpdatePerson Tests


        [Fact]
        public void UpdatePerson_NullPerson()
        {
            //Arrange
            UpdatePersonDto? person_update_request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _personService.UpdatePerson(person_update_request);
            });
        }


        [Fact]
        public void UpdatePerson_InvalidPersonID()
        {
            //Arrange
            UpdatePersonDto? person_update_request = new UpdatePersonDto() { Id = Guid.NewGuid() };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _personService.UpdatePerson(person_update_request);
            });
        }


        //When PersonName is null, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_PersonNameIsNull()
        {
            //Arrange
            AddCountryRequest country_add_request = new AddCountryRequest() { Name = "UK" };
            CountryResponse country_response_from_add = _countryService.Add(country_add_request);

            AddPersonDto person_add_request = new AddPersonDto() { Name = "hamada", CountryId = country_response_from_add.Id, Address = "Abc road", DateOfBirth = DateTime.Parse("2000-01-01"), Email = "abc@example.com", Gender = GenderOptions.Male, ReceiveNewsLetters = true };
            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);

            UpdatePersonDto person_update_request = person_response_from_add.ToUpdatePersonDto();
            person_update_request.Name = null;


            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _personService.UpdatePerson(person_update_request);
            });

        }


        //First, add a new person and try to update the person name and email
        [Fact]
        public void UpdatePerson_PersonFullDetailsUpdation()
        {
            //Arrange
            AddCountryRequest country_add_request = new AddCountryRequest() { Name = "UK" };
            CountryResponse country_response_from_add = _countryService.Add(country_add_request);

            AddPersonDto person_add_request = new AddPersonDto() { Name = "John", CountryId = country_response_from_add.Id, Address = "Abc road", DateOfBirth = DateTime.Parse("2000-01-01"), Email = "abc@example.com", Gender = GenderOptions.Male, ReceiveNewsLetters = true };

            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);

            UpdatePersonDto person_update_request = person_response_from_add.ToUpdatePersonDto();
            person_update_request.Name = "William";
            person_update_request.Email = "william@example.com";

            //Act
            PersonResponse person_response_from_update = _personService.UpdatePerson(person_update_request);

            PersonResponse? person_response_from_get = _personService.GetById(person_response_from_update.Id);

            //Assert
            Assert.Equal(person_response_from_get, person_response_from_update);

        }

        #endregion

        #region DeletePersonById Tests

        //If you supply an valid PersonID, it should return true
        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            //Arrange
            AddCountryRequest country_add_request = new AddCountryRequest() { Name = "USA" };
            CountryResponse country_response_from_add = _countryService.Add(country_add_request);

            AddPersonDto person_add_request = new AddPersonDto() { Name = "Jones", Address = "address", CountryId = country_response_from_add.Id, DateOfBirth = Convert.ToDateTime("2010-01-01"), Email = "jones@example.com", Gender = GenderOptions.Male, ReceiveNewsLetters = true };

            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);


            //Act
            bool isDeleted = _personService.DeleteById(person_response_from_add.Id);

            //Assert
            Assert.True(isDeleted);
        }


        //If you supply an invalid PersonID, it should return false
        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            //Act
            bool isDeleted = _personService.DeleteById(Guid.NewGuid());

            //Assert
            Assert.False(isDeleted);
        }

        #endregion

        private List<PersonResponse> AddDumyPersons()
        {
            AddCountryRequest country_request_1 = new AddCountryRequest() { Name = "USA" };
            AddCountryRequest country_request_2 = new AddCountryRequest() { Name = "India" };

            CountryResponse country_response_1 = _countryService.Add(country_request_1);
            CountryResponse country_response_2 = _countryService.Add(country_request_2);

            AddPersonDto person_request_1 = new AddPersonDto() { Name = "Smith", Email = "smith@example.com", Gender = GenderOptions.Male, Address = "address of smith", CountryId = country_response_1.Id, DateOfBirth = DateTime.Parse("2002-05-06"), ReceiveNewsLetters = true };

            AddPersonDto person_request_2 = new AddPersonDto() { Name = "Mary", Email = "mary@example.com", Gender = GenderOptions.Female, Address = "address of mary", CountryId = country_response_2.Id, DateOfBirth = DateTime.Parse("2000-02-02"), ReceiveNewsLetters = false };

            AddPersonDto person_request_3 = new AddPersonDto() { Name = "Rahman", Email = "rahman@example.com", Gender = GenderOptions.Male, Address = "address of rahman", CountryId = country_response_2.Id, DateOfBirth = DateTime.Parse("1999-03-03"), ReceiveNewsLetters = true };

            List<AddPersonDto> person_requests = new List<AddPersonDto>() { person_request_1, person_request_2, person_request_3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();




            foreach (AddPersonDto person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            return person_response_list_from_add;
        }
    }
}
