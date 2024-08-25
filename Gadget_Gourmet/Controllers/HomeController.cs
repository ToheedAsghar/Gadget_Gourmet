using Gadget_Gourmet.Models.Entities;
using Gadget_Gourmet.Models.Repositories;
using Gadget_Gourmet.Models.Interface;
using Gadget_Gourmet.Models.Repositories;
using Gadget_Gourmet.Extensions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;


namespace Gadget_Gourmet.Controllers
{
	public class HomeController : Controller
	{
		public string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GadgetGourmetDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        //private readonly IHistory _Ihistory;
        //public HomeController(IHistory history)
        //{
        //	_Ihistory = history;
        //}

        private readonly IHistory _history;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IHistory history, UserManager<IdentityUser> userManager)
        {
            _history = history;
            _userManager = userManager;
        }
        public IActionResult Index()
		{
            //_Ihistory.TrackPageVisit("Home", Url.Action("Index", "Home"), System.DateTime.Now.ToString());
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                _history.TrackPageVisit("Home Page", Url.Action("Index", "Home"), DateTime.Now.ToString("f"));
            }
            return View();
		}

		public IActionResult Privacy()
		{
			//_Ihistory.TrackPageVisit("Privacy", Url.Action("Privacy", "Home"), System.DateTime.Now.ToString());
			return View();
		}
		public IActionResult ContactUs()
		{
			//_Ihistory.TrackPageVisit("ContactUs", Url.Action("ContactUs", "Home"), System.DateTime.Now.ToString());
			return View();
		}

		[HttpPost]
		public IActionResult ContactUs(string? email, string? message)
		{
			// msgs -- not implemented
			// direct to our email client
			return RedirectToAction("ContactUs", "Home");
		}

        public IActionResult History()
        {
            // Retrieve the list of visited pages from the session
            var pagesVisited = HttpContext.Session.GetObjectFromJson<List<History>>("PagesVisited") ?? new List<History>();

            return View(pagesVisited);
        }


    }
}
