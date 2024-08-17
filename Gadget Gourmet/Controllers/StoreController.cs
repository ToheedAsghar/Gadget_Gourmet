using Microsoft.AspNetCore.Mvc;

namespace Gadget_Gourmet.Controllers
{
	public class StoreController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
