using Microsoft.AspNetCore.Mvc;
using Services;
using ServicesContracts;
using ServicesContracts.DTO.PersonDtos;

namespace CRUDSection.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [Route("persons/index")]
        [Route("/")]
        public IActionResult Index(string searchBy , string? searchFor)
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
      {
        { nameof(PersonResponse.Name), "Person Name" },
        { nameof(PersonResponse.Email), "Email" },
        { nameof(PersonResponse.DateOfBirth), "Date of Birth" },
        { nameof(PersonResponse.Gender), "Gender" },
        { nameof(PersonResponse.CountryId), "Country" },
        { nameof(PersonResponse.Address), "Address" }
      };
            List<PersonResponse> list_of_persons = _personService.GetFilterdPersons(searchBy, searchFor);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchFor = searchFor;


            return View(list_of_persons);
        }
    }
}
