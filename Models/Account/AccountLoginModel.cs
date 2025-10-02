using System.ComponentModel.DataAnnotations;

namespace test_app.Models.Account
{
    public class AccountLoginModel
    {
        [Required]
        [Display(Name = "E-Posta")]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [Display(Name = "Parola")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; } = true;
    }
}
