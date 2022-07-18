using System;

namespace RestaurantListingAPI.Ioc
{
    public class OperationService : ITransientService, IScopedService, ISingletonService
    {
        private Guid _id;

        public OperationService()
        {
            _id = Guid.NewGuid();  
        }

        public Guid GetOperation()
        {
            return _id;
        }
    }
}
