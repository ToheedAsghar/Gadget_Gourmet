using Gadget_Gourmet.Models.Entities;
using Gadget_Gourmet.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Gadget_Gourmet.Controllers
{
	public class Users : Controller
	{
		public IActionResult Index()
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
			if(user.UserName == "None")
			{
				user.UserName = user.Email;
			}
			else
			{
				user.Email = user.UserName;
			}

            IUserRepository repo = new IUserRepository();
            bool retVal = repo.Login(user);

            if (retVal)
            {
                return RedirectToAction("Profile", "Users");
            }
            else
            {
                return RedirectToAction("Login", "Users");
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
			string[] strs;
			string? Data = string.Empty;
			CookieOptions options = new()
			{
				Expires = DateTime.Now.AddDays(7)
			};

			if (HttpContext.Request.Cookies.ContainsKey("flag"))
			{
				Data = HttpContext.Request.Cookies["flag"];
				strs = Data.Split("|");
			}
			else
			{
				Data = user.UserName + "|" + user.Email;
				HttpContext.Response.Cookies.Append("flag", Data, options);
				strs = new string[] { user.UserName, user.Email };
			}

			IUserRepository repo = new IUserRepository();

			bool retVal = !repo.IdExists(user) && repo.Signup(user);

			if (retVal)
			{
				return RedirectToAction("Index", "Home", new { values = strs });
			}
			else
			{
				return RedirectToAction("Signup", "Users");
			}
		}

		public IActionResult Cart()
		{
			return View();
		}

		public IActionResult Profile()
		{
            User user = new User("None", "None", "None", "None", "none", "None", "None", DateOnly.MinValue);
			return View(user);
		}

        public IActionResult EditProfile()
        {
            return View();
        }
	}
}
