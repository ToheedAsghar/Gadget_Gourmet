using Gadget_Gourmet.Models.Entities;
using Gadget_Gourmet.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Gadget_Gourmet.Controllers
{
	public class AdminController : Controller
	{
		private readonly IGeneric<Product> _productRepo;
		private readonly IGeneric<User> _userRepo;
		private readonly IGeneric<Category> _categoryRepo;

        public AdminController(IGeneric<Product> productRepo, IGeneric<User> userRepo, IGeneric<Category> categoryRepo)
		{
			_productRepo = productRepo;
			_userRepo = userRepo;
            _categoryRepo = categoryRepo;
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
			User check = _userRepo.GetById(id);
			if (check != null)
			{
				try
				{
					return RedirectToAction("UpdateUserDetails", "Admin", new { Id = id });
				}
				catch (Exception ex)
				{
					ViewBag.ErrorMessage = "Something Went Wrong on Our End!";
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
		public IActionResult UpdateUserDetails(int Id)
		{
			User user = _userRepo.GetById(Id);

			return View(user);
		}

        [HttpPost]
        public IActionResult UpdateUserDetails(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = _userRepo.GetById(user.Id);
                    if (existingUser == null)
                    {
                        ViewBag.ErrorMessage = "User not found!";
                        return View(user);
                    }

                    // Update fields
                    existingUser.Name = string.IsNullOrEmpty(user.Name) ? existingUser.Name : user.Name;
                    existingUser.UserName = string.IsNullOrEmpty(user.UserName) ? existingUser.UserName : user.UserName;
                    existingUser.Address = string.IsNullOrEmpty(user.Address) ? existingUser.Address : user.Address;
                    existingUser.Email = string.IsNullOrEmpty(user.Email) ? existingUser.Email : user.Email;
                    existingUser.Phone = string.IsNullOrEmpty(user.Phone) ? existingUser.Phone : user.Phone;
                    existingUser.DateOfBirth = user.DateOfBirth;
                    existingUser.Gender = string.IsNullOrEmpty(user.Gender) ? existingUser.Gender : user.Gender;
                    existingUser.Password = user.Password;

                    _userRepo.Update(existingUser);
                    return RedirectToAction("Index", "Admin", new { msg = "User Updated Successfully!" });
                }
                catch (Exception)
                {
                    ViewBag.ErrorMessage = "Something Went Wrong on Our End!";
                    return View(user);
                }
            }

            ModelState.AddModelError(string.Empty, "Please Resolve the Errors and Try Again!");
            return View(user);
        }


        /* View All Users */
        public IActionResult AllUsers()
		{
			List<User> users = _userRepo.GetAll().ToList();
			return View(users);
		}

		#endregion

		#region Category CRUD Operations

		#region Add Category
		[HttpGet]
        public IActionResult AddCategory(int? id)
        {
            Category category;
            if (id.HasValue && id.Value > 0)
            {
				category = _categoryRepo.GetById(id.Value) ?? new Category();
            }
            else
            {
                category = new Category();
                category.Name = String.Empty;
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Resolve the Errors and Try Again!");
                return View();
            }

            _categoryRepo.Insert(category);

            string msg = "Category Added Successfully!";
            return RedirectToAction("Index", "Admin", new { message = msg });
        }
		#endregion

		#region Remove Category
		[HttpGet]
        public IActionResult RemoveCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RemoveCategory(int id)
        {
            Category check = _categoryRepo.GetById(id);
            if (check != null)
            {
                try
                {
                    _categoryRepo.Delete(id);
                    return RedirectToAction("Index", "Admin", new { msg = "Category Removed Successfully!" });
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "An error occurred while trying to remove the Category";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Category not found!";
                return View();
            }
        }
		#endregion

		#region Update Category

		[HttpGet]
        public IActionResult UpdateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCategory(int id)
        {
            Category check = _categoryRepo.GetById(id);
            if (check != null)
            {
                try
                {
                    return RedirectToAction("UpdateCategoryDetails", "Admin", new { Id = id });
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Something Went Wrong on Our End!";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Category not found!";
                return View();
            }
        }

        [HttpGet]
        public IActionResult UpdateCategoryDetails(int Id)
        {
            Category category = _categoryRepo.GetById(Id);
            if (category != null)
            {
                return View(category);
            }
            else
            {
                return RedirectToAction("UpdateCategory", "Admin");
            }
        }

        [HttpPost]
        public IActionResult UpdateCategoryDetails(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingCategory = _categoryRepo.GetById(category.Id);
                    if (existingCategory == null)
                    {
                        ViewBag.ErrorMessage = "Category not found!";
                        return View(category);
                    }

					existingCategory.Name = string.IsNullOrEmpty(category.Name) ? category.Name : category.Name;

                    _categoryRepo.Update(existingCategory);
                    return RedirectToAction("Index", "Admin", new { msg = "Category Updated Successfully!" });
                }
                catch (Exception)
                {
                    ViewBag.ErrorMessage = "Something Went Wrong on Our End!";
                    return View(category);
                }
            }

            ModelState.AddModelError(string.Empty, "Please Resolve the Errors and Try Again!");
            return View(category);
        }

		#endregion

		
		public IActionResult AllCategories()
        {
            List<Category> categories  = _categoryRepo.GetAll().ToList();
            return View(categories);
        }

        #endregion
    }
}
