using Microsoft.AspNetCore.Mvc;
using ServicesLayer;
namespace _09_DIsection.Controllers
{
    public class HomeController : Controller
    {
        private readonly CitiesService _citiesService;

       public HomeController()
        {
            _citiesService = new CitiesService();
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<string> citites = _citiesService.GetCitites();
            return View(citites);
        }
    }
}
