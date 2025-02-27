using ServicesContracts.DTO.CountryDTOs;

namespace ServicesContracts
{
    /// <summary>
    /// Represents business logic for manipulating Country entity
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// This Method To Add the Country To the countries List
        /// </summary>
        /// <param name="addCountryRequest"></param>
        /// <returns>it will return the Country Response including the Generated Id </returns>
        /// <exception cref="NotImplementedException"></exception>
        CountryResponse Add(AddCountryRequest addCountryRequest);

        /// <summary>
        /// Return all the countries
        /// </summary>
        /// <returns>List of countries</returns>
        List<CountryResponse> GetAllCountries();
    }
}
