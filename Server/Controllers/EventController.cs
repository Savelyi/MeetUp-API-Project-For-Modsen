using AutoMapper;
using Contracts;
using Entities.DTO.EventDto;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<IActionResult> GetAllEventsAsync([FromQuery]EventParameters eventParameters)
        {
            var _events = await _repositoryManager.Events.GetAllEventsAsync(eventParameters,true);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(_events.MetaData));
            var _eventsDto = _mapper.Map<IEnumerable<EventToShowDto>>(_events);
            return Ok(_eventsDto);
        }

        [AllowAnonymous]
        [HttpGet("ShowById/{eventId}")]
        public async Task<IActionResult> GetEventByIdAsync([FromRoute] string eventId)
        {
            var _event = _mapper.Map<EventToShowDto>(await _repositoryManager.Events.GetEventByIdAsync(Guid.Parse(eventId), true));
            return (_event != null) ? Ok(_event) : NotFound("There is no event with such Id");
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateEventAsync([FromBody] EventToCreateDto eventDto)
        {
            var _event = _mapper.Map<Event>(eventDto);
            _event.OrganizerId = User.FindFirst(e => e.Type == "Id").Value;
            _repositoryManager.Events.CreateEvent(_event);
            await _repositoryManager.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("DeleteAny/{eventId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAnyEventAsync([FromRoute] string eventId)
        {
            var _event = await _repositoryManager.Events.GetEventByIdAsync(Guid.Parse(eventId), false);
            if (_event == null)
            {
                return NotFound("There is no event with such Id");
            }
            _repositoryManager.Events.DeleteEvent(_event);
            await _repositoryManager.SaveAsync();
            return NoContent();
        }


        [HttpDelete("Delete/{eventId}")]
        public async Task<IActionResult> DeleteUserEventAsync([FromRoute] string eventId)
        {
            var _event = await _repositoryManager.Events.GetEventByIdAsync(Guid.Parse(eventId), false);
            if (_event == null)
            {
                return NotFound("There is no event with such Id");
            }
            if(_event.OrganizerId!= User.FindFirst(e => e.Type == "Id").Value)
            {
                return Forbid();
            }
            _repositoryManager.Events.DeleteEvent(_event);
            await _repositoryManager.SaveAsync();
            return NoContent();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("UpdateAny/{eventId}")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> UpdateAnyEventAsync([FromBody] EventToUpdateDto eventDto, [FromRoute] string eventId)
        {
            var _event = await _repositoryManager.Events.GetEventByIdAsync(Guid.Parse(eventId), false);
            if (_event == null)
            {
                return NotFound("There is no event with such Id");
            }
            var _eventToUpdate = _mapper.Map<Event>(eventDto);
            _eventToUpdate.Id = Guid.Parse(eventId);
            _eventToUpdate.OrganizerId = _event.OrganizerId;
            _repositoryManager.Events.UpdateEvent(_eventToUpdate);
            await _repositoryManager.SaveAsync();
            return NoContent();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("Update/{eventId}")]
        public async Task<IActionResult> UpdateUserEventAsync([FromBody] EventToUpdateDto eventDto, [FromRoute] string eventId)
        {
            var _event = await _repositoryManager.Events.GetEventByIdAsync(Guid.Parse(eventId), false);
            if (_event == null)
            {
                return NotFound("There is no event with such Id");
            }
            if (_event.OrganizerId != User.FindFirst(e => e.Type == "Id").Value)
            {
                return Forbid();
            }
            var _eventToUpdate = _mapper.Map<Event>(eventDto);
            _eventToUpdate.Id = Guid.Parse(eventId);
            _eventToUpdate.OrganizerId = _event.OrganizerId;
            _repositoryManager.Events.UpdateEvent(_eventToUpdate);
            await _repositoryManager.SaveAsync();
            return NoContent();
        }

    }
}
