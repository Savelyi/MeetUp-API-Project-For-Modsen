using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly ILoggerManager _logger;
        readonly UserManager<User> _userManager;
        public AuthenticationController(IMapper mapper, ILoggerManager logger, UserManager<User> userManager)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }
    }
}
