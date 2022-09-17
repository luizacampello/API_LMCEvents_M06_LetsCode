using LMCEvents.Core.Interfaces;
using LMCEvents.DTOs;
using LMCEvents.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMCEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("/events/{local}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<EventResponseDTO>> GetEventsByLocal(string local)
        {
            List<EventResponseDTO> eventResponse = _cityEventService.GetCityEventsByLocal(local);

            if (eventResponse.Count == 0)
            {
                return NotFound();
            }

            return Ok(eventResponse);
        }

        [HttpGet("/eventByTitle/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<EventResponseDTO>> GetEventByTitle(string title)
        {
            List <EventResponseDTO> eventResponse = _cityEventService.GetEventByTitle(title);

            if (eventResponse.Count == 0)
            {
                return NotFound();
            }

            return Ok(eventResponse);
        }

        [HttpGet("/eventByLocalAndDate/{local}/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<EventResponseDTO>> GetEventByLocalAndDate(string local, DateTime date)
        {
            List<EventResponseDTO> eventResponse = _cityEventService.GetEventByLocalAndDate(local, date);

            if (eventResponse.Count == 0)
            {
                return NotFound();
            }

            return Ok(eventResponse);
        }

        [HttpGet("/eventByPriceAndDate/{priceMin}/{priceMax}/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<EventResponseDTO>> GetEventByPriceAndDate(decimal priceMin, decimal priceMax, DateTime date)
        {
            List<EventResponseDTO> eventResponse = _cityEventService.GetEventByPriceAndDate(priceMin, priceMax, date);

            if (eventResponse.Count == 0)
            {
                return NotFound();
            }

            return Ok(eventResponse);
        }

        [HttpPost("/newEvent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "admin")]
        public ActionResult<EventResponseDTO> PostNewEvent(EventResponseDTO eventResponse)
        {
            if (!_cityEventService.InsertEvent(eventResponse))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(PostNewEvent), eventResponse);
        }

        [HttpPut("/updateEvent/{idEvent}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ServiceFilter(typeof(ValidateEventIdActionFilter))]
        [Authorize(Roles = "admin")]
        public ActionResult<EventResponseDTO> UpdateEvent(EventResponseDTO eventResponse, long idEvent)
        {
            if (!_cityEventService.UpdateEvent(idEvent, eventResponse))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete("/deleteEvent/{idEvent}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ServiceFilter(typeof(ValidateEventIdActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteEvent(long idEvent)
        {
            if (!_cityEventService.DeleteEvent(idEvent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }



    }
}
