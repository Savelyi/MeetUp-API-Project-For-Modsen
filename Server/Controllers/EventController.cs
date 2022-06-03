using AutoMapper;
using Contracts;
using Entities.DTO.EventDto;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {

        readonly IRepositoryManager _repositoryManager;
        readonly ILoggerManager _loggerManager;
        readonly IMapper _mapper;
        readonly UserManager<User> _userManager;
        public EventController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper, UserManager<User> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
        }

        [AllowAnonymous]
        [HttpGet("ShowAll")]
        public async Task<IActionResult> ShowEvents()
        {
            var _events = _mapper.Map<IEnumerable<EventToShowDto>>(await _repositoryManager.Events.GetAllEventsAsync(true));
            return Ok(_events);
        }

        [AllowAnonymous]
        [HttpGet("ShowById/{eventId}")]
        public async Task<IActionResult> GetEventById([FromRoute] string eventId)
        {
            var _event = _mapper.Map<EventToShowDto>(await _repositoryManager.Events.GetEventByIdAsync(Guid.Parse(eventId), true));
            return Ok(_event);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateEvent([FromBody] EventToCreateDto eventDto)
        {
            var _event = _mapper.Map<Event>(eventDto);
            _event.OrganizerId = User.FindFirst(e => e.Type == "Id").Value;
            _repositoryManager.Events.CreateEvent(_event);
            await _repositoryManager.SaveAsync();
            return StatusCode(201);
        }


    }
}
