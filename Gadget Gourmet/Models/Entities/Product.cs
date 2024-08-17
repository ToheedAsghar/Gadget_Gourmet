namespace Gadget_Gourmet.Models.Entities
{
    public class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Weight { get; set; }
        public decimal Price { get; set; } = 0;
        public string? Color { get; set; }
        public string? Manufacturer { get; set; }
        public int Category { get; set; }

        public int Quantity { get; set; }

        public Product(string? name, string? desc, string? weight, string? color,string? manufacturer,int category)
        {
            Name = name;
            Description = desc;
            Weight = weight;
            Color = color;
            Manufacturer = manufacturer;
            Category = category;
        }
    }
}
