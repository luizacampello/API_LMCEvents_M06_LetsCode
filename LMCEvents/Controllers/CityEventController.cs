using LMCEvents.Core.Interfaces;
using LMCEvents.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            List <EventResponseDTO> eventResponse = _cityEventService.GetEventByTitle(title);

            if (eventResponse.Count == 0)
            {
                return NotFound();
            }

            return Ok(eventResponse);
        }

        [HttpGet("/eventByLocalAndDate/{local}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<EventResponseDTO>> GetEventByLocalAndDate(string local, DateTime date)
        {
            List<EventResponseDTO> eventResponse = _cityEventService.GetEventByLocalAndDate(local, date);

            if (eventResponse.Count == 0)
            {
                return NotFound();
            }

            return Ok(eventResponse);
        }

        [HttpGet("/eventByPriceAndDate/{priceMin}/{priceMax}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        public ActionResult<EventResponseDTO> PutEvent(EventResponseDTO eventResponse, long idEvent)
        {
            if (!_cityEventService.UpdateEvent(idEvent, eventResponse))
            {
                return BadRequest();
            }

            return NoContent();
        }



    }
}
