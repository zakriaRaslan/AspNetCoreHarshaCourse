using Microsoft.AspNetCore.Mvc;
using PartialViewSection.Models;

namespace PartialViewSection.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]    
        public IActionResult Index()
        {
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            ViewData["ListTitle"] = "Cities";
            ViewData["ListItems"] = new List<string>()
            {
                "Cairo",
                "Qlayubia",
                "Alex",
                "Fayum",
                "Luxor",
            };

            return View();
        }

        [Route("programming-lang-list")]
        public IActionResult Programminglanguages()
        {
            ListModel myList = new ListModel()
            {
                ListTitle = "Programming Languages",
                ListItems = new List<string>()
            {
                "C#",
                "Python",
                "Java",
                "Java script",
                "Ruby",
                "C++",
                "C",
            }
            };


            return PartialView("_ListPartialView" , myList);
        }
    }
}
