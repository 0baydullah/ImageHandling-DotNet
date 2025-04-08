using System.ComponentModel.DataAnnotations;

namespace ImageHandling.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int Price { get; set; }

        [Required]
        public IFormFile photo { get; set; } = null!;
        // comment added
        // added another comment
        // added another comment
        // adding final comment
    }
}
