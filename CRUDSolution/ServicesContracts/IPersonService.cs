using ServicesContracts.DTO.Helpers;
using ServicesContracts.DTO.PersonDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContracts
{
    public interface IPersonService
    {
        PersonResponse AddPerson(AddPersonDto? personToAdd);

        List<PersonResponse> GetAllPersons();
        PersonResponse? GetById(Guid? id);


        /// <summary>
        /// This method to get the filterd person under the condition of the filter
        /// </summary>
        /// <param name="searchBy">The key to fillter by</param>
        /// <param name="searchFor">The value to search for</param>
        /// <returns>it should return the person who is the search key of him matching the search value </returns>
        List<PersonResponse> GetFilterdPersons(string searchBy , string? searchFor);

        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortedKey, SortOrderOption sortOption);

        PersonResponse UpdatePerson(UpdatePersonDto? personToUpdate);

        bool DeleteById(Guid? id);
    }
}
