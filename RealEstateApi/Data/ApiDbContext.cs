using Microsoft.EntityFrameworkCore;
using RealEstateApi.Models;

namespace RealEstateApi.Data
{
    public class ApiDbContext:DbContext
    {
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Property> properties { get; set; } 
        public DbSet<User> users { get; set; }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\ProjectModels;Database=RealEstateDb");
        }

    }
}
