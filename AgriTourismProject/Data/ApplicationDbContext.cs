using AgriTourismProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AgriTourismProject.Data
{
    
   
        public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {

            }

            // Categories table will be created
            public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
               new Category
               {
                   Id = 1,
                   Name = "Farm Stay",
                   Location = "Countryside",
                   Cost = 100.00,
                   Capacity = 10,
                   Date = new DateTime(2024, 1, 15)
               },
            new Category
            {
                Id = 2,
                Name = "Vineyard Tour",
                Location = "Wine Country",
                Cost = 150.00,
                Capacity = 20,
                Date = new DateTime(2024, 2, 20)
            }
                );
        }


    }

    }

