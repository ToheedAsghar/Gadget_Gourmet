using Microsoft.AspNetCore.Mvc;

namespace Gadget_Gourmet.Controllers
{
	public class Users : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Cart()
		{
			return View();
		}
	}
}
