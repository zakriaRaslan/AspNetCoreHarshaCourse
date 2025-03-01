using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServicesContracts.DTO.CountryDTOs
{
    /// <summary>
    /// This is the return type of the most methods in CountryService
    /// </summary>
    public class CountryResponse : CountryBase
    {
        public Guid Id { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(CountryResponse)) return false;

            CountryResponse countryToCompare = (CountryResponse)obj;

            return this.Id == countryToCompare.Id && this.Name == countryToCompare.Name;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap around
            {
                int hash = 17; // Prime number
                hash = hash * 23 + Id.GetHashCode(); // Prime number
                hash = hash * 23 + (Name?.GetHashCode() ?? 0); // Null-safe           
                return hash;
            }
        }
    }
}
