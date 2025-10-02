using Microsoft.AspNetCore.Identity;

namespace test_app.Context
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; } = null!;
    }
}