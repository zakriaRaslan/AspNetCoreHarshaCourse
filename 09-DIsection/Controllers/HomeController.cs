using Microsoft.AspNetCore.Mvc;
using ServicesLayer;
using ServicesContract;
namespace _09_DIsection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService _citiesService;

        public HomeController(ICitiesService citiesService)
        {
            _citiesService = citiesService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<string> citites = _citiesService.GetCities();
            return View(citites);
        }

        #region Add Service To Method directly 
        //[Route("/")]
        //public IActionResult Index([FromServices] ICitiesService _citiesService)
        //{
        //    List<string> citites = _citiesService.GetCities();
        //    return View(citites);
        //}
        #endregion
    }
}
