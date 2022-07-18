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
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationsController(ILogger<LocationsController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllLocations")]
        public async Task<IActionResult> GetAllLocations()
        {
            try
            {
                _logger.LogDebug("[LocationsController:GetLocations] Started");
                var locations = await _unitOfWork.Locations.GetAll(include: q => q.Include(location => location.Restaurant));

                var mapped = _mapper.Map<IList<LocationDTO>>(locations);
                _logger.LogDebug("[LocationsController:GetLocations] Finished");
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[LocationsController:GetLocations] Failed to get Locations");
                return BadRequest(ex);
            }
        }

        [HttpGet("GetLocationsByOrder")]
        public async Task<IActionResult> GetLocationsByOrder()
        {
            try
            {
                _logger.LogDebug("[LocationsController:GetLocationsByOrder] Started");
                var locations = await _unitOfWork.Locations.GetAll(orderBy: q => q.OrderBy(location => location.Id));

                var mapped = _mapper.Map<IList<LocationDTO>>(locations);
                _logger.LogDebug("[LocationsController:GetLocationsByOrder] Finished");
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[LocationsController:GetLocationsByOrder] Failed to get Locations");
                return BadRequest(ex);
            }
        }

  

        [HttpGet("GetLocationById/{id:int}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            try
            {
                _logger.LogDebug("[LocationsController:GetLocationById] Started");
                var locations = await _unitOfWork.Locations.Get(
                    expression: q => q.Id == id,
                    include: x => x.Include(location => location.Restaurant)
                    );

                var mapped = _mapper.Map<LocationDTO>(locations);
                _logger.LogDebug("[LocationsController:GetLocationById] Finished");
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[LocationsController:GetLGetLocationByIdocations] Failed to get Locations");
                return BadRequest(ex);
            }
        }

    }
}
