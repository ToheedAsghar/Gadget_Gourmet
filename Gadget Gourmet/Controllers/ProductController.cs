using Gadget_Gourmet.Models.Entities;
using Gadget_Gourmet.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Gadget_Gourmet.Controllers
{
    public class ProductController : Controller
    {
        protected readonly IGeneric<Product> _productRepo;

        public ProductController(IGeneric<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public IActionResult Index(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ViewProduct(int id)
        {
            Product prod = _productRepo.GetById(id);
            if(prod != null)
            {
                return RedirectToAction("ViewProductDetails", "Product");
            }
            else
            {
                return View();
            }
        }

        public IActionResult ViewProductDetails(Product product)
        {
            return View();
        }

    }
}
