using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using ServicesContracts;
using ServicesContracts.DTO.CountryDTOs;
using ServicesContracts.DTO.Helpers;
using ServicesContracts.DTO.PersonDtos;
using System.Globalization;

namespace CRUDSection.Controllers
{
    [Route("persons")]
    public class PersonsController : Controller
    {


        private readonly IPersonService _personService;
        private readonly ICountryService _countryService;

        public PersonsController(IPersonService personService, ICountryService countryService)
        {
            _personService = personService;
            _countryService = countryService;
        }

        [Route("index")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchFor, string sortBy = nameof(PersonResponse.Name), SortOrderOption sortOrder = SortOrderOption.ASC)
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

            //Sort
            List<PersonResponse> sortedPersons = _personService.GetSortedPersons(list_of_persons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();


            return View(sortedPersons);
        }


        [HttpGet("create")]
        public IActionResult Create()
        {
            List<CountryResponse> list_of_countries = _countryService.GetAllCountries();
            ViewBag.CountriesList = list_of_countries;

            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(AddPersonDto addPersonDto)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = _countryService.GetAllCountries();
                ViewBag.CountriesList = countries;

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            //call the service method
            PersonResponse personResponse = _personService.AddPerson(addPersonDto);

            //navigate to Index() action method (it makes another get request to "persons/index"
            return RedirectToAction("Index", "Persons");
        }

    }
}
