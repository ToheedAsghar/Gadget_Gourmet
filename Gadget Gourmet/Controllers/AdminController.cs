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

		/* Add Product */

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
				ModelState.AddModelError(string.Empty, "Resolve the Errors and Try Again!");
				return View();
			}

			_productRepo.Insert(prod);

			string msg = string.Empty;
			return RedirectToAction("Index", "Admin", new { message = msg });
		}

		/* Remove Product */

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
				try
				{
					_productRepo.Delete(id);
					return RedirectToAction("Index", "Admin", new { msg = "Product Removed Successfully!" });
				}
				catch (Exception ex)
				{
					ViewBag.ErrorMessage = "An error occurred while trying to remove the product.";
					return View();
				}
			}
			else
			{
				ViewBag.ErrorMessage = "Product not found!";
				return View();
			}
		}


		/* Update Product */

		[HttpGet]
		public IActionResult UpdateProduct()
		{
			return View();
		}

        [HttpPost]
        public IActionResult UpdateProduct(int id)
        {

            Product check = _productRepo.GetById(id);
            if (check != null)
            {
                try
                {
					return RedirectToAction("UpdateProductDetails", "Admin", new { Id=id });
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Something Went Wrong on Our End!";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Product not found!";
                return View();
            }
        }

        [HttpGet]
        public IActionResult UpdateProductDetails(int Id)
        {
            Product product = _productRepo.GetById(Id);
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProductDetails(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingProduct = _productRepo.GetById(product.Id);
                    if (existingProduct == null)
                    {
                        ViewBag.ErrorMessage = "Product not found!";
                        return View(product);
                    }

                    // Update the fields you want to allow modification, but ensure the Id remains intact
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Weight = product.Weight;
                    existingProduct.Price = product.Price;
                    existingProduct.Color = product.Color;
                    existingProduct.Manufacturer = product.Manufacturer;
                    existingProduct.Category = product.Category;
                    existingProduct.Quantity = product.Quantity;

                    _productRepo.Update(existingProduct);
                    return RedirectToAction("Index", "Admin", new { msg = "Product Updated Successfully!" });
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Something Went Wrong on Our End!";
                    return View(product);
                }
            }

            ModelState.AddModelError(string.Empty, "Please Resolve the Errors and Try Again!");
            return View(product);
        }

		/* View All Products */
		public IActionResult AllProducts()
		{
			List<Product> products = _productRepo.GetAll().ToList();
			return View(products);
		}
        #endregion
    }
}
