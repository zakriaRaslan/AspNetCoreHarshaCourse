using _08_ViewComponentSection.Models;
using Microsoft.AspNetCore.Mvc;

namespace _08_ViewComponentSection.ViewComponents
{
    // if you Does'y write the surrfix "ViewComponent" After the component name you must write the attribute
    //[ViewComponent]
    public class GridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(GridPersonModel grid)
        {
          

            //ViewData["Grid"] = gridPersonModel;
            return View(grid);// this will search for the view in 
            // Views/Shared/Component/{name of the Class = Grid}/Default.cshtml.
            // to change the name of the view use
            // return View("Name Of View")
        }
    }
}
