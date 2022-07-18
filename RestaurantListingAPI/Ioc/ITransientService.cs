using System;

namespace RestaurantListingAPI.Ioc
{
    public interface ITransientService
    {
        Guid GetOperation();
    }
}
