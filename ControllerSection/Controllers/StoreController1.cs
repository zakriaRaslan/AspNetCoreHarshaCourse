using ControllerSection.Models;
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

        #region Model Binding
        [Route("store/shoes/{shoesId?}/{islogin?}")]
        public IActionResult Shoes(int? shoesId , [FromRoute]bool? islogin , Shoes shoes)    
        {
            // You can get the values from one of there Form fields , Request body , Route Value , Query string 
            // to specific one of this to get from in query string and Route value you can use the attribute [FromQuery] or [FromRoute] befor the parmeter
            // this parameter of shoesId and islogin is QueryString
            // and the Route has the more priority of Querystring
            if (!shoesId.HasValue)
            {
                
                return BadRequest("The Shose id is not supplied");
            }

            if (shoesId ==null)
            {
             
                return BadRequest("The shoes id is can't be null");
            }
           
            if (shoesId <= 0)
            {
                
                return NotFound("The id can't be 0 or negative number");
            }

            if (shoesId > 1000)
            {
                
                return NotFound("The id it can't be bigger than 1000");
            }

            if (islogin == false)
            {
              
                return Unauthorized(" You Must be logged in");
            }

            return Content($"The Shoes Id is => {shoesId}", "text/plain");
        }


        #endregion
    }
}
