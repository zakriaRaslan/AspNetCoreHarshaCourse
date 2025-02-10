using Microsoft.AspNetCore.Mvc;
using ViewExample.models;
using ViewExample.models.Wrappers;

namespace ViewExample.Controllers
{
    public class ProductController : Controller
    {
        [Route("product/all")]
        public IActionResult All()
        {
            IEnumerable<Product> products = new List<Product>()
            {
                new Product(){Id = 1 , title = "Mobile"},
                new Product(){Id = 2 , title = "Screen"},
                new Product(){Id = 3 , title = "PC"},
                new Product(){Id = 4 , title = "Clothes"},
            };
            ViewData["title"] = "Display All Products";
            return View(products);
        }

        [Route("    ")]
        public IActionResult ProductsAndPersons()
        {
            IEnumerable<Product> products = new List<Product>()
            {
                new Product(){Id = 1 , title = "Mobile"},
                new Product(){Id = 2 , title = "Screen"},
                new Product(){Id = 3 , title = "PC"},
                new Product(){Id = 4 , title = "Clothes"},
            };

            List<Person> pepole = new List<Person>
            {
                new Person() { Name = "Sayed", BirthDate = Convert.ToDateTime("1995 - 05 - 15") , PersonGender= Gender.Male },
                new Person() { Name = "Fathi", BirthDate = Convert.ToDateTime("2000 - 07 - 20") , PersonGender= Gender.Male },
                new Person() { Name = "Mai",BirthDate = Convert.ToDateTime("2002 - 03 - 17"),  PersonGender= Gender.Female },
            };

            PersonAndProductWrapper personAndProductWrapper = new PersonAndProductWrapper()
            {
                PersonsData = pepole,
                ProductsDate = products,
            };

            ViewData["title"] = "Display All Products And Persons";
            return View(personAndProductWrapper);
        }

    }
}
