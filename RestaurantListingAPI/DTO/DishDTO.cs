using RestaurantListingAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace RestaurantListingAPI.DTO
{
    public class CreateDishDTO
    {
        [Required]
        [StringLength(maximumLength: 10, ErrorMessage = "Dish name is too long")]
        public string Name { get; set; }
        [Required]
        [Range(1, 5)]
        public int Stars { get; set; }

        public string Review { get; set; }
        [Required]
        public int RestaurantId { get; set; }
    }

    public class DishDTO : CreateDishDTO
    {
        public int Id { get; set; }
        public RestaurantDTO Restaurant { get; set; }
    }

    public class UpdateDishDTO : CreateDishDTO
    {
    }
}
