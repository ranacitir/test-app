
using System.ComponentModel.DataAnnotations;

namespace test_app.Context
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength (100)]
        [Required]
        public string Name { get; set; } = null!;
        public int Mg { get; set; }
        public int Dose { get; set; }
        public int TotalMg { get; set; }

        [MaxLength(100)]
        public string? Image { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        [MaxLength(1000)]
        public string? SideEffects { get; set; }
        public bool Status { get; set; }
        

    }
}
