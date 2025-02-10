using Microsoft.AspNetCore.Mvc;
using ViewExample.models;

namespace ViewExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "View Example";

            List<Person> pepole = new List<Person>
            {
                new Person() { Name = "Sayed", BirthDate = Convert.ToDateTime("1995 - 05 - 15") , PersonGender= Gender.Male },
                new Person() { Name = "Fathi", BirthDate = Convert.ToDateTime("2000 - 07 - 20") , PersonGender= Gender.Male },
                new Person() { Name = "Mai",BirthDate = Convert.ToDateTime("2002 - 03 - 17"),  PersonGender= Gender.Female },
            };
            //ViewData["pepole"] = pepole;
            ViewBag.pepole = pepole;
            return View(); // will get the view from Views/home/Index.cshtml
        }
    }
}
