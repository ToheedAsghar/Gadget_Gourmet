using Microsoft.AspNetCore.Mvc;

namespace Gadget_Gourmet.Controllers
{
	public class Store : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Product(int id)
		{
			return View();
		}
	}
}
