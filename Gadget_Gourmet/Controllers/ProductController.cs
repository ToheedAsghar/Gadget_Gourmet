using Gadget_Gourmet.Data;
using Gadget_Gourmet.Models.Entities;
using Gadget_Gourmet.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Gadget_Gourmet.Controllers
{
    public class ProductController : Controller
    {
        protected readonly IGeneric<Product> _productRepo;
        private readonly ApplicationDbContext _context;

        public ProductController(IGeneric<Product> productRepo, ApplicationDbContext context)
        {
            _productRepo = productRepo;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewProduct(int id)
        {
            Product prod = _productRepo.GetById(id);
            if (prod != null)
            {
                return View("ViewProductDetails", prod);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult ViewProductDetails(Product product)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string searchString)
        {
            List<Product> products = new List<Product>();
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GadgetGourmetDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            Product prod = new();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT * FROM [dbo].[product] WHERE [name] LIKE '%' + @s + '%';";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@s", searchString);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prod = new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Weight = reader["Weight"] != DBNull.Value ? Convert.ToDecimal(reader["Weight"]) : 0,
                                Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0,
                                Color = reader["Color"].ToString(),
                                Manufacturer = reader["Manufacturer"].ToString(),
                                Category = reader["Category"].ToString(),
                                Quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 0
                            };

                            products.Add(prod);
                        }
                    }
                }
            }

            return View("_ProductListPartial", products);
        }

        public IActionResult ProductSearchBar()
        {
            return View();
        }
    }
}
