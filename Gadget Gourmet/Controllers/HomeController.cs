using Gadget_Gourmet.Models;
using Gadget_Gourmet.Models.Entities;
using Gadget_Gourmet.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace Gadget_Gourmet.Controllers
{
	public class HomeController : Controller
	{
		public string connectionString = string.Empty;
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

		// register user
		public IActionResult Signup()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Signup(User user)
		{
			IUserRepository repo = new IUserRepository();
			bool retVal = repo.Signup(user);

			if (retVal)
			{
				return RedirectToAction("Index", "Home");
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
