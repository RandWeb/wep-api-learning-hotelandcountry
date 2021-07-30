using AutoMapper;
using HotelListing.Data.Entities;
using HotelListing.Models;
using HotelListing.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountController(
            UserManager<User> userManager,
            ILogger<AccountController> logger,
            IMapper mapper,
            IAuthManager authManager)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }
        [HttpPost()]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            _logger.LogInformation($"Regestration Attemt for {userDto.Email}");
            
            try
            {
                var userDTO = _mapper.Map<UserDTO>(userDto);
                var user = _mapper.Map<User>(userDTO);
                user.UserName = user.Email;
                var result = await _userManager.CreateAsync(user,userDto.Password);
                if (!result.Succeeded)
                {
                   foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code,error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await _userManager.AddToRolesAsync(user,userDTO.Roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Somthing went Wrong in the{nameof(Register)}");
                return StatusCode(500,$"Something Went Wrong in the {nameof(Register)}");
            }
            
            return Accepted($"{userDto} Added");
        }
        [HttpPost()]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDto)
        {
            _logger.LogInformation($"Login Attemt for {loginUserDto.Email}");
            
            try
            {
                if (!await _authManager.ValidateUser(loginUserDto))
                    return Unauthorized("User login Attempt Failed");
                else
                    return Accepted(new {Token = await _authManager.CreateToken() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Somthing went Wrong in the {nameof(Login)}");
                return StatusCode(500,$"Something Went Wrong in the {nameof(Login)}");
            }
            
        }
    }


}
