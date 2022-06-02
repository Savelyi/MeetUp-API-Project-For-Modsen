using AutoMapper;
using Contracts;
using Entities.DTO.EventDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        readonly IRepositoryManager _repositoryManager;
        readonly ILoggerManager _loggerManager;
        readonly IMapper _mapper;
        public HomeController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
        }

        [HttpGet("Events")]
        public async Task<IActionResult> ShowEvents()
        {
            try
            {
                var events = _mapper.Map<IEnumerable<EventToShowDto>>(await _repositoryManager.Events.GetAllEventsAsync(false));
                return Ok(events);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"{nameof(ShowEvents)} - {ex}");
                return StatusCode(500);
            }
        }
    }
}
