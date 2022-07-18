using System;

namespace RestaurantListingAPI.Ioc
{
    public interface IScopedService
    {
        Guid GetOperation();
    }
}
