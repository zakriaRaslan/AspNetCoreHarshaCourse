using Services;
using ServicesContracts;
using ServicesContracts.DTO.CountryDTOs;
using Xunit;
namespace CRUDTests
{
    /// <summary>
    /// this class to test all the methods of the country services
    /// </summary>
    public class CountryServicesTests
    {
        private readonly ICountryService _countryService;
        public CountryServicesTests()
        {
            _countryService = new CountryServices(false);
        }

        #region AddCountry Test Methods
        // test if the addCountry method will throw the ArgumentNullExeption when the the addCountry Param is null
        [Fact]
        public void AddCountry_addCountryParamIsNull()
        {
            //Arrang
            AddCountryRequest addCountryRequest = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _countryService.Add(addCountryRequest);
            });
        }

        // test if the the addCountry method will throw the ArgumenNullExeption if the name of the country is null
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //Arrang
            AddCountryRequest addCountryReq = new() { Name = null };

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _countryService.Add(addCountryReq);
            });
        }

        // test if the the addCountry method will throw the ArgumenExeption if the name of the country i dublicate
        [Fact]
        public void AddCountry_CountryNameIsDublicate()
        {
            //Arrang
            AddCountryRequest addCountryReq1 = new() { Name = "EGYPT" };
            AddCountryRequest addCountryReq2 = new() { Name = "EGYPT" };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countryService.Add(addCountryReq1);
                _countryService.Add(addCountryReq2);
            });

        }

        // test if the the addCountry method will return the country response when it save the country to the list

        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            //Arrang
            AddCountryRequest addCountryReq = new() { Name = "EGYPT" };

            //Act
            var response = _countryService.Add(addCountryReq);
            List<CountryResponse> countriesFromGetAllCountries = _countryService.GetAllCountries();

            //Assert
            Assert.True(response.Id != Guid.Empty);
            Assert.Contains(response,countriesFromGetAllCountries);
        }
        #endregion

        #region GetAllCountries Test Methods

        //The list of countries is not be null
        [Fact]
        public void GetAllCountries_ListNotNull()
        {
            //Act
            var response = _countryService.GetAllCountries();

            //Assert
            Assert.NotNull(response);
        }

        //Get the list after adding a few countries
        [Fact]
        public void GetAllCountries_AfterAddFewCountries()
        {
            //Arrang
            List<AddCountryRequest> someCountriesToAdd = new List<AddCountryRequest>()
            {
                new AddCountryRequest(){Name = "Egypt"},
                new AddCountryRequest(){Name = "SaudiArabia"},
            };

            List<CountryResponse> expectedCountriesList = new List<CountryResponse>();
            //Act
            foreach (AddCountryRequest addCountryReq in someCountriesToAdd)
            {
                expectedCountriesList.Add(_countryService.Add(addCountryReq));
            }

            List<CountryResponse> actualCountriesResponseList = _countryService.GetAllCountries();

            foreach (CountryResponse expectedCountry in expectedCountriesList)
            {
                Assert.Contains(expectedCountry, actualCountriesResponseList);
            }
        }


        #endregion

        #region GetById Test Methods

        [Fact]
        public void GetById_WithNullId()
        {
            //Arrang
             Guid? id = null;

            //Act
            CountryResponse ResponseFromGetById = _countryService.GetById(id);

            //Assert
            Assert.Null(ResponseFromGetById);
        }

        [Fact]
        public void GetById_WithValidId() 
        {
            //Arrang
            AddCountryRequest addCountryReq = new AddCountryRequest() { Name = "Egypt"};

            CountryResponse responseFromAddCountry = _countryService.Add(addCountryReq);
            CountryResponse responseFromGetById = _countryService.GetById(responseFromAddCountry.Id);

            //Assert
            Assert.Equal(responseFromAddCountry, responseFromGetById);

        }


        #endregion


    }
}
