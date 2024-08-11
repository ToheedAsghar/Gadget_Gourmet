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

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(User user)
		{
			IUserRepository repo = new IUserRepository();
			bool retVal = repo.Login(user);

			if (retVal) {
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return RedirectToAction("Login", "Home");
			}
		}

		[HttpGet]
		public IActionResult Signup()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Signup(User user)
		{
			String[] strs = new String[1];
			CookieOptions options = new()
			{
				Expires = System.DateTime.Now.AddDays(7)
			};

			string? Data = string.Empty;
			if (HttpContext.Request.Cookies.ContainsKey("flag"))
			{
                HttpContext.Response.Cookies.Append("flag", Data, options);
				Data = HttpContext.Request.Cookies["flag"];
				strs = Data.Split("|");
            }
            else
			{
				Data = user.UserName + "|" + user.Email;
				HttpContext.Response.Cookies.Append("flag", Data, options);
			}

			IUserRepository repo = new IUserRepository();
			bool retVal = repo.Signup(user);

			if (retVal)
			{
				return RedirectToAction("Index", "Home", strs);
			}
			else
			{
				return RedirectToAction("Signup", "Home");
			}
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
