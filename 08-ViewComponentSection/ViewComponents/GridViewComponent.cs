using _08_ViewComponentSection.Models;
using Microsoft.AspNetCore.Mvc;

namespace _08_ViewComponentSection.ViewComponents
{
    // if you Does'y write the surrfix "ViewComponent" After the component name you must write the attribute
    //[ViewComponent]
    public class GridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            GridPersonModel gridPersonModel = new GridPersonModel()
            {
                GridTitle = "Persons List",
                Persons = new List<Person>()
                {
                    new Person(){Name="Sayed" , JopTitle="Back-end Developer"},
                    new Person(){Name="Yahia" , JopTitle="Full-stack Developer"},
                    new Person(){Name="Mariam" , JopTitle="Front-end Developer"},
                    new Person(){Name="Mostafa" , JopTitle="Mobile Developer"},
                }
            };

            //ViewData["Grid"] = gridPersonModel;
            return View(gridPersonModel);// this will search for the view in 
            // Views/Shared/Component/{name of the Class = Grid}/Default.cshtml.
            // to change the name of the view use
            // return View("Name Of View")
        }
    }
}
