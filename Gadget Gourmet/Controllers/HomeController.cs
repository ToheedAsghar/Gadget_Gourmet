using Gadget_Gourmet.Models.Entities;
using Gadget_Gourmet.Models.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Gadget_Gourmet.Controllers
{
	public class HomeController : Controller
	{
		public string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GadgetGourmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult ContactUs()
		{
			return View();
		}

		[HttpPost]
		public IActionResult ContactUs(string? email, string? message)
		{
			// msgs -- not implemented
			// direct to our email client
			return RedirectToAction("ContactUs", "Home");
		}

	}
}
