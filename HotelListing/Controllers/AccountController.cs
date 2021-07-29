using AutoMapper;
using HotelListing.Data.Entities;
using HotelListing.Models;
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
        //private readonly SignInManager<User> _singInManaiger;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<User> userManager,
          //  SignInManager<User> singInManaiger,
            ILogger<AccountController> logger,
            IMapper mapper)
        {
            _userManager = userManager;
         //   _singInManaiger = singInManaiger;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpPost()]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserCommand userCommand)
        {
            _logger.LogInformation($"Regestration Attemt for {userCommand.Email}");
            
            try
            {
                var userDTO = _mapper.Map<UserDTO>(userCommand);
                var user = _mapper.Map<User>(userDTO);
                var result = await _userManager.CreateAsync(user,userCommand.Password);
                if (!result.Succeeded)
                {
                   foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code,error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Somthing went Wrong in the{nameof(Register)}");
                return StatusCode(500,$"Something Went Wrong in the {nameof(Register)}");
            }
            return Accepted($"{userCommand} Added");
        }
        [HttpPost()]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserCommand userCommand)
        {
            _logger.LogInformation($"Login Attemt for {userCommand.Email}");
            
            try
            {
                //var user = _mapper.Map<User>(userCommand);
         //       var result = await _singInManaiger.PasswordSignInAsync(userCommand.Email,userCommand.Password,false,false);
               /* if (!result.Succeeded)
                {
                    return Unauthorized("User login Attempt Failed");
                }*/
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Somthing went Wrong in the {nameof(Login)}");
                return StatusCode(500,$"Something Went Wrong in the {nameof(Login)}");
            }
            return Accepted();
        }
    }


}
