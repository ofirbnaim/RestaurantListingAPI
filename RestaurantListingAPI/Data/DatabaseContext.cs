
using Microsoft.EntityFrameworkCore;

namespace RestaurantListingAPI.Data
{
    public class DatabaseContext : DbContext
    {
        //list all tables = DbSets
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Location> Locationss { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DatabaseContext(DbContextOptions options) :base(options)
        {
        }
    }
}

