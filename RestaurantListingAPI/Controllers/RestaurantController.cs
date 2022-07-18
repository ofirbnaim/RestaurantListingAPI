using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantListingAPI.DTO;
using RestaurantListingAPI.Reposetories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<RestaurantController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RestaurantController(ILogger<RestaurantController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllRestaurants")]
        public async Task<IActionResult> GetAllRestaurants()
        {
            try
            {
                _logger.LogDebug("[RestaurantController:GetAllRestaurants] Started");
                var restaurants = await _unitOfWork.Restaurants.GetAll(include: q => q.Include(restaurant => restaurant.Dishes)
                                                                                      .Include(restaurant => restaurant.Location));

                var mapped = _mapper.Map<IList<RestaurantDTO>>(restaurants);
                _logger.LogDebug("[RestaurantController:GetAllRestaurants] Finished");
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[RestaurantController:GetAllRestaurants] Failed to get Restaurants");
                return BadRequest(ex);
            }
        }

        [HttpGet("GetResterauntById/{id:int}")]
        public async Task<IActionResult> GetResterauntById(int id)
        {
            try
            {
                _logger.LogDebug("[RestaurantController:GetResterauntById] Started");
                var restaurants = await _unitOfWork.Restaurants.Get(
                    expression: q => q.Id == id,
                    include: x => x.Include(restaurant => restaurant.Dishes)
                                   .Include(restaurant => restaurant.Location)
                    );

                var mapped = _mapper.Map<RestaurantDTO>(restaurants);
                _logger.LogDebug("[RestaurantController:GetResterauntById] Finished");
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[RestaurantController:GetResterauntById] Failed to get Restaurants");
                return BadRequest(ex);
            }
        }
    }
}
