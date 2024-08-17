using Gadget_Gourmet.Models.Entities;
using Gadget_Gourmet.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Gadget_Gourmet.Controllers
{
	public class AdminController : Controller
	{
		private readonly IGeneric<Product> _productRepo;

		public AdminController(IGeneric<Product> productRepo)
		{
			_productRepo = productRepo;
		}

		// In the index page, admin will be shown all CRUD Operations
		public IActionResult Index(string Message)
		{
			return View();
		}

		#region Product CRUD Operations

		public IActionResult AddProduct(int? id)
		{
			Product prod;
			if (id.HasValue && id.Value > 0)
			{
				prod = _productRepo.GetById(id.Value) ?? new Product();
			}
			else
			{
				prod = new Product();
			}
			return View(prod);
		}
		[HttpPost]
		public IActionResult AddProduct(Product prod)
		{
			if (!ModelState.IsValid)
			{
				// If the model is invalid, return the same view with validation messages
				ModelState.AddModelError(string.Empty, "Resolve the Errors and Try Again!");
				return View();
			}

			_productRepo.Insert(prod);

			string msg = string.Empty;
			return RedirectToAction("Index", "Admin", new { message = msg });
		}

		[HttpGet]
		public IActionResult RemoveProduct()
		{
			return View();
		}

		[HttpPost]
		public IActionResult RemoveProduct(int id)
		{
			Product check = _productRepo.GetById(id);
			if (check != null)
			{
				_productRepo.Delete(id);
				return RedirectToAction("Index", "Admin", new { msg = "Product Removed Successfully!" });
			}
			else
			{
				return View();
			}
		}

		#endregion
	}
}
