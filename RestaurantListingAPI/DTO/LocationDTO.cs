using RestaurantListingAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace RestaurantListingAPI.DTO
{
    public class CreateLocationDTO
    {
        [Required]
        [StringLength(maximumLength: 15, ErrorMessage = "Country name is too long")]
        public string Country { get; set; }
        [Required]
        [StringLength(maximumLength: 15, ErrorMessage = "Address is too long")]
        public string Address { get; set; }
        [Required]
        public int RestaurantId { get; set; }
    }

    public class LocationDTO : CreateLocationDTO
    {
        public int Id { get; set; }
        public RestaurantDTO Restaurant { get; set; }
    }

    public class UpdateLocationDTO : LocationDTO
    {
    }
}
