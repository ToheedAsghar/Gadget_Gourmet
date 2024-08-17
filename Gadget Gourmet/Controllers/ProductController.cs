using Microsoft.AspNetCore.Mvc;

namespace Gadget_Gourmet.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(int id)
        {
            return View();
        }

    }
}
