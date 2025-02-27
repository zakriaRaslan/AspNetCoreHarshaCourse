using Entities;
using ServicesContracts;
using ServicesContracts.DTO.CountryDTOs;

namespace Services
{
    public class CountryServices : ICountryService
    {
        private List<Country> _countries;
        public CountryServices()
        {
            _countries = new List<Country>();
        }

        public CountryResponse Add(AddCountryRequest addCountryDto)
        {
            if (addCountryDto == null)
            {
                throw new ArgumentNullException(nameof(addCountryDto));
            }

            if (addCountryDto.Name == null)
            {
                throw new ArgumentNullException(nameof(addCountryDto.Name));
            }
            
            if(_countries.Where(temp => temp.Name == addCountryDto.Name).Count() > 0)
            {
                throw new ArgumentException("The country name is already exist!");
            }

            Country country = addCountryDto.ToCountry();

            country.Id = Guid.NewGuid();

            _countries.Add(country);

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }
    }
}
