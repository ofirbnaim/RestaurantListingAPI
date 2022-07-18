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
    public class DishesController : ControllerBase
    {
        private readonly ILogger<DishesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DishesController(ILogger<DishesController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllDishes")]
        public async Task<IActionResult> GetAllDishes()
        {
            try
            {
                _logger.LogDebug("[DishesController:GetAllDishes] Started");
                var dishes = await _unitOfWork.Dishes.GetAll(include: q => q.Include(dishe => dishe.Restaurant));

                var mapped = _mapper.Map<IList<DishDTO>>(dishes);
                _logger.LogDebug("[DishesController:GetAllDishes] Finished");
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[DishesController:GetAllDishes] Failed to get Dishes");
                return BadRequest(ex);
            }
        }

        [HttpGet("GetDishesByOrder")]
        public async Task<IActionResult> GetDishesByOrder()
        {
            try
            {
                _logger.LogDebug("[DishesController:GetDishesByOrder] Started");
                var dishes = await _unitOfWork.Dishes.GetAll(orderBy: q => q.OrderBy(dishe => dishe.Id));

                var mapped = _mapper.Map<IList<DishDTO>>(dishes);
                _logger.LogDebug("[DishesController:GetDishesByOrder] Finished");
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[DishesController:GetDishesByOrder] Failed to get Dishes");
                return BadRequest(ex);
            }
        }


        [HttpGet("GetDisheById/{id:int}")]
        public async Task<IActionResult> GetDisheById(int id)
        {
            try
            {
                _logger.LogDebug("[DishesController:GetDisheById] Started");
                var dishes = await _unitOfWork.Dishes.Get(
                    expression: q => q.Id == id,
                    include: x => x.Include(dishe => dishe.Restaurant)
                    );

                var mapped = _mapper.Map<DishDTO>(dishes);
                _logger.LogDebug("[DishesController:GetDisheById] Finished");
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[DishesController:GetDisheById] Failed to get Dish");
                return BadRequest(ex);
            }
        }
    }
}
