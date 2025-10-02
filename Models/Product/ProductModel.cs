using System.ComponentModel.DataAnnotations;
using test_app.Context;

namespace test_app.Models.Product
{
    public class ProductModel
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = null!;
        public int Mg { get; set; }
        public int Dose { get; set; }
        public int TotalMg { get; set; }

        [MaxLength(100)]
        public IFormFile? Image { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }
        public int CategoryId { get; set; }

        [MaxLength(1000)]
        public string? SideEffects { get; set; }
        public bool Status { get; set; }

    }
}
