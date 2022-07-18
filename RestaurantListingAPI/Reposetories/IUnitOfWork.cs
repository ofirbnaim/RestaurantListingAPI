using RestaurantListingAPI.Data;
using System;
using System.Threading.Tasks;

namespace RestaurantListingAPI.Reposetories
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Restaurant> Restaurants { get;}
        public IGenericRepository<Location> Locations { get; }
        public IGenericRepository<Dish> Dishes { get;}

        Task Save();
    }
}
