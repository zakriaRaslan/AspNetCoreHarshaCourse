using Microsoft.AspNetCore.Mvc;

namespace ConfigurationSection.Controllers
{
    public class HomeController: Controller
    {

        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [Route("/")]
        public IActionResult Index()
        {

            ViewBag.key1 = _configuration.GetValue<string>("MyKey", "The Default Value");
            return View();
        }
    }
}
