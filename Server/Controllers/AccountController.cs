using AutoMapper;
using Contracts;
using Entities.DTO.UserDto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Filters;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly ILoggerManager _logger;
        readonly UserManager<User> _userManager;
        readonly IAuthenticationManager _authenticationManger;

        public AccountController(IMapper mapper, ILoggerManager logger, UserManager<User> userManager, IAuthenticationManager authenticationManger)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _authenticationManger = authenticationManger;
        }

        /// <summary>
        /// SignUp
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <response code="400">Some fields in Model are nullable or error was occured while adding new User in DB</response>
        /// <response code="201">User Created</response>
        /// <response code="422">Model Validation Error</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [HttpPost("SignUp")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> SignUp([FromBody]UserForSignUpDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRoleAsync(user, userDto.Role);
            return StatusCode(201);
        }

        /// <summary>
        /// Sign In 
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>UserName And JWT</returns>
        /// <response code="200">User Signed in and got JWT</response>
        /// <response code="404">Unauthorized</response>
        /// <response code="400">Some fields in Model are nullable</response>
        /// <response code="422">Model Validation Error</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [HttpPost("SignIn")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate(UserForSignInDto userDto)
        {
            if(!await _authenticationManger.ValidateUser(userDto))
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed, wrong username or password");
                return Unauthorized();
            }
            var result = new
            {
                UserName=userDto.UserName,
                Token =await _authenticationManger.CreateToken()
            };
            return Ok(result);
        }

    }
}
