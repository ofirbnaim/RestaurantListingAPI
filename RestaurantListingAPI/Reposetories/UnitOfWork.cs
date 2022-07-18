using RestaurantListingAPI.Data;
using System;
using System.Threading.Tasks;

namespace RestaurantListingAPI.Reposetories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IGenericRepository<Restaurant> _restaurants;
        private IGenericRepository<Location> _locations;
        private IGenericRepository<Dish> _dishes;

        private readonly DatabaseContext _dbContext;

        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IGenericRepository<Restaurant> Restaurants => _restaurants ??= new GenericRepository<Restaurant>(_dbContext);
        public IGenericRepository<Location> Locations => _locations ??= new GenericRepository<Location>(_dbContext);
        public IGenericRepository<Dish> Dishes => _dishes ??= new GenericRepository<Dish>(_dbContext);
        
        
        
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
