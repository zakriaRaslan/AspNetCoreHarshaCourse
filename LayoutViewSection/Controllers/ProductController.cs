using Microsoft.AspNetCore.Mvc;

namespace LayoutViewSection.Controllers
{
    public class ProductController : Controller
    {
        [Route("all-products")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("product-search/{productId?}")]
        public IActionResult Search(int? productId)
        {
            ViewBag.ProductId = productId;
            return View();
        }
        [Route("product-order")]
        public IActionResult Order()
        {
            return View();
        }



    }
}
