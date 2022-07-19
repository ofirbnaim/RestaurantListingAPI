using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantListingAPI.Data;
using RestaurantListingAPI.DTO;
using RestaurantListingAPI.Services;
using System;
using System.Linq;
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
        private readonly IAuthManager _authManager;


        public AccountController(ILogger<AccountController> logger, IMapper mapper, UserManager<ApiUser> userManager, IAuthManager authManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
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

               

                var resultCreate = await _userManager.CreateAsync(user, registerUserDTO.Password);

                var resultRoles = await _userManager.AddToRolesAsync(user, registerUserDTO.Roles);


                if (resultCreate.Succeeded && resultRoles.Succeeded)
                {
                    _logger.LogDebug("[AccountController:Register] finished succesfully");
                    return StatusCode(StatusCodes.Status201Created, "❤ User Created succesfully");
                }
                else
                {
                    foreach(var error in resultCreate.Errors.Concat(resultRoles.Errors))
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

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDto)
        {

            _logger.LogInformation($"Registration Attempt for {loginUserDto.Email} ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _authManager.ValidateUser(loginUserDto))
                {
                    return Unauthorized();
                }
                //before refreshing a token simplt return the token in the accepted method
                //anonymous object new {sdsd= dsds}
                return Accepted(new { Token = await _authManager.CreateToken() });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Login)}");
                return Problem($"Something Went Wrong in the {nameof(Login)}", statusCode: 500);
            }
        }
    }
}
