using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantListingAPI.Data;
using RestaurantListingAPI.DTO;
using System;
using System.Threading.Tasks;

namespace RestaurantListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AccountController(ILogger<AccountController> logger, IMapper mapper, UserManager<ApiUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            try
            {
                _logger.LogDebug("[AccountController:Register] Started");

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ApiUser user = _mapper.Map<ApiUser>(registerUserDTO);
                user.UserName = registerUserDTO.Email;
                user.PhoneNumber = registerUserDTO.MobileNumber;

               

                var result = await _userManager.CreateAsync(user, registerUserDTO.Password);

                if (result.Succeeded)
                {
                    _logger.LogDebug("[AccountController:Register] finished succesfully");
                    return StatusCode(StatusCodes.Status201Created, "❤ User Created succesfully");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    _logger.LogDebug("[AccountController:Register] finished with error");
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[AccountController:Register] Failed to Register");
                return BadRequest();
            }

        }
    }
}
