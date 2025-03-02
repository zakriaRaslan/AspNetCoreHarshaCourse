using Entities;
using ServicesContracts;
using ServicesContracts.DTO.CountryDTOs;

namespace Services
{
    public class CountryServices : ICountryService
    {
        private readonly List<Country> _countries;
        public CountryServices(bool initialize = true)
        {
            _countries = new List<Country>();

            if (initialize)
            {
                _countries.AddRange(new List<Country>() {
                    new Country() {  Id = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B"), Name = "Egypt" },

                    new Country() { Id = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F"), Name = "Saudi Arabia" },

                    new Country() { Id = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E"), Name = "Libya" },

                    new Country() { Id = Guid.Parse("15889048-AF93-412C-B8F3-22103E943A6D"), Name = "Palestine" },

                    new Country() { Id = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB"), Name = "Tunisia" }
                  });
            }
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

            if (_countries.Where(temp => temp.Name == addCountryDto.Name).Count() > 0)
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

        public CountryResponse? GetById(Guid? id)
        {
            if (id == null)
                return null;

            Country? countryFromList = _countries.FirstOrDefault(country => country.Id == id);
            if (countryFromList == null) return null;

            return countryFromList.ToCountryResponse();
        }
    }
}
