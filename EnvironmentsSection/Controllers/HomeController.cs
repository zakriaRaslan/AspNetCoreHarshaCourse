using Microsoft.AspNetCore.Mvc;

namespace EnvironmentsSection.Controllers
{
    public class HomeController(IWebHostEnvironment webHostEnvironment) : Controller
    {
        [Route("/")]
        [Route("same-route")]
        public IActionResult Index()
        {
            //webHostEnvironment.IsDevelopment();
            ViewBag.Environment = webHostEnvironment.EnvironmentName;
            return View();
        }

        [Route("same-route")]
        public IActionResult other() 
        {
            return View();
        }
    }
}
