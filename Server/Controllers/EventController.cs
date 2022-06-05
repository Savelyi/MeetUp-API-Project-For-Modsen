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

        /// <summary>
        /// Get All Events
        /// </summary>
        /// <param name="eventParameters"></param>
        /// <returns>List of EventsDto</returns>
        /// <response code="200">Returns List of Events</response>
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpGet("ShowAll")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllEventsAsync([FromQuery]EventParameters eventParameters)
        {
            var _events = await _repositoryManager.Events.GetAllEventsAsync(eventParameters,true);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(_events.MetaData));
            var _eventsDto = _mapper.Map<IEnumerable<EventToShowDto>>(_events);
            return Ok(_eventsDto);
        }

        /// <summary>
        /// Get Any Event By Id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>EventDto</returns>
        /// <response code="200">Return EventDto</response>
        /// <response code="404">Event with such Id was not found</response>
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpGet("ShowById/{eventId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetEventByIdAsync([FromRoute] string eventId)
        {
            var _event = _mapper.Map<EventToShowDto>(await _repositoryManager.Events.GetEventByIdAsync(Guid.Parse(eventId), true));
            return (_event != null) ? Ok(_event) : NotFound("There is no event with such Id");
        }

        /// <summary>
        /// Create Event
        /// </summary>
        /// <param name="eventDto"></param>
        /// <returns></returns>
        /// <response code="201">Event Created</response>
        /// <response code="400">Some fields in Model are nullable</response>
        /// <response code="422">Model Validation Error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("Create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateEventAsync([FromBody] EventToCreateDto eventDto)
        {
            var _event = _mapper.Map<Event>(eventDto);
            _event.OrganizerId = User.FindFirst(e => e.Type == "Id").Value;
            _repositoryManager.Events.CreateEvent(_event);
            await _repositoryManager.SaveAsync();
            return StatusCode(201);
        }

        /// <summary>
        /// Delete Any Event (For Admins) By Id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Event with such not found</response>
        /// <response code="204">Event succesfully deleted</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("DeleteAny/{eventId}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Delete User Event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">User tried to delete event which does not belong him</response>
        /// <response code="404">Event with such not found</response>
        /// <response code="204">Event succesfully deleted</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("Delete/{eventId}")]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Update Any Event(For Admin) By Id
        /// </summary>
        /// <param name="eventDto"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Event with such not found</response>
        /// <response code="204">Event succesfully deleted</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="400">Some fields in Model are nullable</response>
        /// <response code="422">Model Validation Error</response>
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("UpdateAny/{eventId}")]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
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

        /// <summary>
        /// Update User Event
        /// </summary>
        /// <param name="eventDto"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">User tried to update event which does not belong him</response>
        /// <response code="404">Event with such not found</response>
        /// <response code="204">Event succesfully deleted</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="400">Some fields in Model are nullable</response>
        /// <response code="422">Model Validation Error</response>
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("Update/{eventId}")]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
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
