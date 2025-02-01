using Microsoft.AspNetCore.Mvc;
using ModelValidationExapmle.Models;
using ModelValidationExapmle.CustomModelBinders;
namespace ModelValidationExapmle.Controllers
{
    public class HomeController : Controller
    {
        [Route("register")]
        // To specifiy Some Property To only Bind You can use Bind attribute To Avoid Overposting from and hacker or malware
        //[Bind(nameof(person.Name) + "," + (nameof(person.Password) + "," +
        //    nameof(person.ConfirmPassword) + "," + nameof(person.Email)))]
        //[ModelBinder(BinderType = typeof(PersonModelBinder))] // ignored it after used the modelBinderProvider => PersonBinderProvider
        public IActionResult Index(Person person , [FromHeader(Name = "User-Agent")] string userAgent)
        {
            if (!ModelState.IsValid)
            {
                #region Handel Errors Without LINQ
                //List<string> errors = new List<string>();
                //foreach (var value in ModelState.Values) 
                //{ 
                //    foreach(var error in value.Errors)
                //    {
                //        errors.Add(error.ErrorMessage);
                //    }
                //}
                //string errorsMessage = string.Join("\n", errors);
                //return BadRequest(errorsMessage); 

                #endregion

                #region Handel Errors By Using LINQ

                string errorMessage = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors)
                    .Select(error => error.ErrorMessage));
                return BadRequest(errorMessage);
                #endregion

            }
            return Content($"{person} , {userAgent}");
        }
    }
}
