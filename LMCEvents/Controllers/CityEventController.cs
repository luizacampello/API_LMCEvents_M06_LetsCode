using LMCEvents.Core.Interfaces;
using LMCEvents.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LMCEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("/events")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<EventResponseDTO>> GetEvents()
        {
            return Ok(_cityEventService.GetCityEvents());
        }

        [HttpGet("/eventByTitle/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<EventResponseDTO>> GetEventByTitle(string title)
        {
            EventResponseDTO eventResponse = _cityEventService.GetEventByTitle(title);

            if (eventResponse is null)
            {
                return NotFound();
            }

            return Ok(eventResponse);
        }

        [HttpPost("/newEvent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EventResponseDTO> PostNewEvent(EventResponseDTO eventResponse)
        {
            if (!_cityEventService.InsertEvent(eventResponse))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(PostNewEvent), eventResponse);
        }


    }
}
