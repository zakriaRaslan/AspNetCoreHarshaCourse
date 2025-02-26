using Microsoft.AspNetCore.Mvc;

namespace 10-EnvironmentSection.Controllers
{
    public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
}
