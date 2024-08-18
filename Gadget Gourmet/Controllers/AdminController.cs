﻿using Gadget_Gourmet.Models.Entities;
using Gadget_Gourmet.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Gadget_Gourmet.Controllers
{
	public class AdminController : Controller
	{
		private readonly IGeneric<Product> _productRepo;
		private readonly IGeneric<User> _userRepo;

		public AdminController(IGeneric<Product> productRepo, IGeneric<User> userRepo)
		{
			_productRepo = productRepo;
			_userRepo = userRepo;
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

		#region User CRUD Operations
		[HttpGet]
		public IActionResult AddUser(int? id)
		{
			User user;
			if (id.HasValue && id.Value > 0)
			{
				user = _userRepo.GetById(id.Value) ?? new User();
			}
			else
			{
				user = new User();
				user.UserName = string.Empty;
				user.Name = string.Empty;
				user.Email = string.Empty;
				user.Phone = string.Empty;
				user.Address = string.Empty;
				user.DateOfBirth = DateTime.MinValue;
				user.Password = string.Empty;
			}
			return View(user);
		}

		[HttpPost]
		public IActionResult AddUser(User user)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError(string.Empty, "Resolve the Errors and Try Again!");
				return View();
			}

			_userRepo.Insert(user);

			string msg = "User Added Successfully!";
			return RedirectToAction("Index", "Admin", new { message = msg });
		}


		[HttpGet]
		public IActionResult RemoveUser()
		{
			return View();
		}

		[HttpPost]
		public IActionResult RemoveUser(int id)
		{
			User check = _userRepo.GetById(id);
			if (check != null)
			{
				try
				{
					_userRepo.Delete(id);
					return RedirectToAction("Index", "Admin", new { msg = "User Removed Successfully!" });
				}
				catch (Exception ex)
				{
					ViewBag.ErrorMessage = "An error occurred while trying to remove the User";
					return View();
				}
			}
			else
			{
				ViewBag.ErrorMessage = "User not found!";
				return View();
			}
		}


		[HttpGet]
		public IActionResult UpdateUser()
		{
			return View();
		}

		[HttpPost]
		public IActionResult UpdateUser(int id)
		{

			Product check = _productRepo.GetById(id);
			if (check != null)
			{
				try
				{
					return RedirectToAction("UpdateProductDetails", "Admin", new { Id = id });
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
		public IActionResult UpdateUserDetails(int Id)
		{
			Product product = _productRepo.GetById(Id);
			return View(product);
		}

		[HttpPost]
		public IActionResult UpdateUserDetails(Product product)
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
		public IActionResult AllUsers()
		{
			List<Product> products = _productRepo.GetAll().ToList();
			return View(products);
		}

		#endregion
	}
}
