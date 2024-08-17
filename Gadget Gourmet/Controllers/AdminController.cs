using Gadget_Gourmet.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gadget_Gourmet.Controllers
{
    public class AdminController : Controller
    {
        // In the index page, admin will be shown all CRUD Operations
        public IActionResult Index()
        {
            return View();
        }

		#region Product CRUD Operations
		[HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

		[HttpPost]
		public IActionResult AddProduct(Product prod)
		{
			return View();
		}


		#endregion


	}
}
