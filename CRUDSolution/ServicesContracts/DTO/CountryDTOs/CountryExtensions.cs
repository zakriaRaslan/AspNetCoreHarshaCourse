using Entities;

namespace ServicesContracts.DTO.CountryDTOs
{
    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse { Id = country.Id, Name = country.Name };
        }
    }
}
