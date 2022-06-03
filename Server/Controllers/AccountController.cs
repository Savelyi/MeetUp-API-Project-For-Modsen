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
