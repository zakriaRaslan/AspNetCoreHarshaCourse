using Entities;

namespace ServicesContracts.DTO.CountryDTOs
{   
    /// <summary>
    /// Dto Class For Adding A New Country
    /// </summary>
    public class AddCountryRequest:CountryBase
    {
        

        public Country ToCountry()
        {
            return new Country { Name = Name };
        }
    }
}
