using Entities;
using ServicesContracts;
using ServicesContracts.DTO.CountryDTOs;

namespace Services
{
    public class CountryServices : ICountryService
    {
        private readonly PersonsDbContext _dbContext;
        public CountryServices(PersonsDbContext dbContext )
        {
            _dbContext = dbContext;

           
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

            if (_dbContext.Countrys.Where(temp => temp.Name == addCountryDto.Name).Count() > 0)
            {
                throw new ArgumentException("The country name is already exist!");
            }

            Country country = addCountryDto.ToCountry();

            country.Id = Guid.NewGuid();

            _dbContext.Countrys.Add(country);
            _dbContext.SaveChanges();
            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _dbContext.Countrys.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetById(Guid? id)
        {
            if (id == null)
                return null;

            Country? countryFromList = _dbContext.Countrys.FirstOrDefault(country => country.Id == id);
            if (countryFromList == null) return null;

            return countryFromList.ToCountryResponse();
        }
    }
}
