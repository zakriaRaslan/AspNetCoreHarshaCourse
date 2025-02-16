using _08_ViewComponentSection.Models;
using Microsoft.AspNetCore.Mvc;

namespace _08_ViewComponentSection.Controllers
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
            return View();
        }

        [Route("display-friends-list")]
        public IActionResult DisplayFriendsList()
        {
            GridPersonModel FrindsList = new GridPersonModel()
            {
                GridTitle = "Friends List",
                Persons = new List<Person>()
                {
                    new Person(){Name="Sayed" , JopTitle="Back-end Developer"},
                    new Person(){Name="Yahia" , JopTitle="Full-stack Developer"},
                    new Person(){Name="Mariam" , JopTitle="Front-end Developer"},
                    new Person(){Name="Mostafa" , JopTitle="Mobile Developer"},
                }
            };

            return ViewComponent("Grid",new {grid = FrindsList});
        }
    }
}
