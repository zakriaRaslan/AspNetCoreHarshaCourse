using Microsoft.AspNetCore.Mvc;

namespace ControllerSection.Controllers
{
    public class StoreController : Controller
    {
        [Route("store/books/{id}")]
        public IActionResult books()
        {
            int id = Convert.ToInt32(Request.RouteValues["id"]);
            return Content($"<h1>Book Store for id = {id} </h1>", "text/html");
        }
    }
}
