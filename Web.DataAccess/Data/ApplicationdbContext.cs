using Microsoft.EntityFrameworkCore;
using Web.Models.Models;

namespace Web.DataAccess.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Category> categories { get; set; }
         public DbSet<Product>Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Category_Id = 1, Name = "action", DisplayOrder = 1 },
                new Category { Category_Id = 2, Name = "scifi", DisplayOrder = 2 },
                new Category { Category_Id= 3, Name = "History", DisplayOrder = 3 }
                );

                modelBuilder.Entity<Product>().HasData(

                    new Product{    Product_Id = 1,Title = "Mahabarath", Author = "Vyasadu", Description= "King of Books", ISBN = "MB123456", ListPrice = 400, Price50 = 300, Price100 = 200},

                    new Product{

                        Product_Id = 2,Title = "BagavithGetha", Author = "Vyasudu", Description= "Teaches the way of life", ISBN = "BG3431234", ListPrice = 300, Price50 = 200, Price100 = 150
                    },
                    new Product{

                        Product_Id = 3,Title = "One indian girl", Author = "Chethan Bagath", Description= "The the life of the indian girl", ISBN = "IG8493793", ListPrice = 100, Price50 = 70, Price100 = 50
                    },
                    new Product{

                        Product_Id = 4,Title = "Half girlfriend", Author = "Bagath", Description= "love story", ISBN = "HG53827283", ListPrice = 150, Price50 = 100, Price100 = 80
                    });



        }
    }
}