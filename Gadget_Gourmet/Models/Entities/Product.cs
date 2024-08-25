using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Gadget_Gourmet.Models.Entities
{
    public class Product
    {
		[Required(ErrorMessage = "Id is required.")]
		[Range(1, int.MaxValue, ErrorMessage = "Id must be a positive number")]
		public int Id { get; set; } = -1;


        [Required(ErrorMessage ="Invalid Name Entered!")]
        public string? Name { get; set; }


        [Required(ErrorMessage ="Description cannot be Empty!")]
        public string? Description { get; set; }

        public decimal Weight { get; set; }


        [Required(ErrorMessage = "Invalid Price!")]
		[Range(1, int.MaxValue, ErrorMessage = "Range must be a positive number")]
		public decimal? Price { get; set; } = 0;


        public string? Color { get; set; }


        [Required(ErrorMessage = "Manufacturer Can't be Empty!")]
        public string? Manufacturer { get; set; }


        public string? Category { get; set; }


        [Required(ErrorMessage ="Please Specify the Quantity of the Products")]
        [Range(1, int.MaxValue, ErrorMessage ="Quantity can't be Negative!")]
        public int? Quantity { get; set; }


		public Product() { }

		public Product(string? name, string? desc, decimal weight, int price, string? color,string? manufacturer,string category, int quantity)
        {
            Name = name;
            Description = desc;
            Weight = weight;
            Color = color;
            Manufacturer = manufacturer;
            Category = category;
            Price = price;
            Quantity = quantity;
        }
    }
}
