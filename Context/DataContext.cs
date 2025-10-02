using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace test_app.Context
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
               new List<Category>()
               {
                    new Category () { Id = 1, Name = "Antidepresan" , Url = "antidepresan"}
               }
            );


            modelBuilder.Entity<Product>().HasData(
               new List<Product>()
               {
                    new Product() {
                        Id = 1, Name = "Anafranil", Mg = 75, Dose = 3, TotalMg = 225,
                        Image = "../img/SpongeBob reaction pic.jpg", Description = "dhcldshlkdlkdjasljsadljlsakjldjksaldja",
                        CategoryId = 1, SideEffects = "", Status = true }
               }
           );
        }
    }
}
