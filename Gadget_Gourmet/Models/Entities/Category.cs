using System.ComponentModel.DataAnnotations;

namespace Gadget_Gourmet.Models.Entities
{
    public class Category
    {
        [Required(ErrorMessage = "Category ID can't be Empty")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name can't be Empty")]
        public string Name { get; set; }

        public Category() {
            Id = -1;
            Name = string.Empty;
        }

        public Category(int id, string name)
        {
            this.Name = name;
            this.Id = id;
        }

    }
}
