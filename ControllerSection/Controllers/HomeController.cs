using ControllerSection.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllerSection.Controllers
{
    public class Homecontroller:Controller
    {
        [Route("home")] // this attribute to specify the url to execute the method1
        [Route("/")]
        //public string Index()
        //{
        //    return "Hello from home";
        //}

        public ContentResult Index()
        {
            // this way is the oldest way and it will work without inheriting from Controller base class

            //return new ContentResult()
            //{
            //    Content = "Hello from Index method ContentResult",
            //    ContentType = "text/plain"
            //};

            // the new way it my using Content() method  but it work after inherint from controller base class
            //return Content("Hello from Index method ContentResult(content Method)", "text/plain");

            //return html 
            return Content("<h1>Welcome</h1> <h2>from Index method<h2>" , "text/html");
            
        }

        [Route("person")]
        public JsonResult Person()
        {
            Person somePerson = new Person() { Id = Guid.NewGuid(), FirstName = "Ahmed", LastName = "Mohamed", Age = 25 };

            //return new JsonResult(somePerson);
            // And you can use 
            return Json(somePerson);
        }

        [Route("about")]
        public string About()
        {
            return "Hello from about";
        }

        [Route("contact-us")]
        public string Contact()
        {
            return "Hello from contact";
        }

        #region FileResult Section
        // You must add the app.UseStaticFiles(); Middleware
        // we have three type of file Results (1) VirtualFileResult , (2) PhysicalFileResult
        // VirtualFileResult => good with files which it located in wwwroot folder
        // PhysicalFileResult => good with files which it located out of the wwwroot
        [Route("file-download1")]
        public VirtualFileResult FileDownload1()
        {
            //return new VirtualFileResult("/file.txt", "text/pain");

            //return new VirtualFileResult("/sample.pdf", "application/pdf");
            return File("/sample.pdf", "application/pdf");
        }
        [Route("file-download2")]
        public PhysicalFileResult FileDownload2()
        {
            //return new PhysicalFileResult(@"D:\courses\0my_projects\HarshaCourse\ControllersSection\sample.pdf", "application/pdf");
            return PhysicalFile(@"D:\courses\0my_projects\HarshaCourse\ControllersSection\sample.pdf", "application/pdf");
        }

        // FileContentResult() return a file from a byte[]
        // its usefull when you read images from data bases and when you encrypting some information in this file
        [Route("file-download3")]
        public FileContentResult FileDownload3()
        {
            byte[] file = System.IO.File.ReadAllBytes(@"D:\courses\0my_projects\HarshaCourse\ControllersSection\sample.pdf");

            //return new FileContentResult(file, "application/pdf");
            return File(file, "application/pdf");
        }

        #endregion

        #region IActionResult

        // The IActionResult is the parent interface for all action result calsses in asp.net core

        // in this example the reson behind using the IActionResult it provide to return any type from it child without change the return type
        [Route("bookstore")]
        public IActionResult book()
        {
            if (!Request.Query.ContainsKey("bookid"))
            {
                //Response.StatusCode = 400;
                //return Content("The book id is not supplied");
                return BadRequest("The book id is not supplied");
            }

            if (string.IsNullOrEmpty(Request.Query["bookid"]))
            {
                //Response.StatusCode = 400;
                //return Content("The book id is can't be null");
                return BadRequest("The book id is can't be null");
            }
            int bookid = Convert.ToInt32(Request.Query["bookid"]);
            if (bookid <= 0)
            {
                //Response.StatusCode = 400;
                //return Content("The id can't be 0 or negative number");
                return NotFound("The id can't be 0 or negative number");
            }

            if(bookid > 1000)
            {
                //Response.StatusCode = 400;
                //return Content("The id it can't be bigger than 1000");
                return NotFound("The id it can't be bigger than 1000");
            }

            if (Convert.ToBoolean(Request.Query["islogin"]) == false)
            {
                //Response.StatusCode = 401;
                //return Content(" You Must be logged in");
                return Unauthorized(" You Must be logged in");
            }

            //return File("/sample.pdf", "application/pdf");


            #region Redirect
            // 301 => to tell the borwsers search engins to keep using the new url and stop supporting the old url
            // 302 => temporary redirection the old url will keep work 

            // Redirect Result => the usecase of it is if we have a real world project and we  need to change 
            // the url of an action like this from bookstore to store/books we can't change the url directly 
            // because it will casuse  some trubles with users so we will use RedirectToActionResult

            //return new RedirectToActionResult("books", "Store", new {id:bookid }); // this will return status code 302 => found
            // Short Cut ↓ 
            //return RedirectToAction("books", "Store", new { id = bookid });

            //return new RedirectToActionResult("books", "Store", new { } , true); // this will return status code 301 => Moved Permanently
            // Short Cut ↓ 
            //return RedirectToActionPermanent("books", "Store", new { id = bookid }); // in the most cases this is the best

            // we have localRedirect() to redirect to locla url in the same project by using url
            //return new LocalRedirectResult($"store/books/{bookid}"); // 302
            // ShortCut
            //return LocalRedirect($"store/books/{bookid}"); //302
            //return LocalRedirectPermanent($"store/books/{bookid}"); //301


            // RedirectResult() use this if you want to redirect to a url in another project
            //return new RedirectToActionResult("books", "Store", new { id = bookid }); //302
            //Shortcut
            //return RedirectToAction("books", "Store", new { id = bookid }); //302
            return RedirectToActionPermanent("books", "Store", new { id = bookid }); // 301


            #endregion


        }

        #endregion
    }
}
