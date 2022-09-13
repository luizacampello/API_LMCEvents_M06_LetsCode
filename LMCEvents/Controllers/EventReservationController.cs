using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using LMCEvents.Core.Service;
using LMCEvents.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LMCEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventReservationController : ControllerBase
    {
        public IEventReservationService _eventReservationService;

        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }


        [HttpGet("/bookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<BookingResponseDTO>> GetBookings()
        {
            return Ok(_eventReservationService.GetBookings());
        }

        [HttpPost("/newBooking/{IdEvent}/{PersonName}/{Quantity}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EventResponseDTO> PostNewBooking(long idEvent, string personName, long quantity)
        {
            if (!_eventReservationService.InsertBooking(idEvent, personName, quantity))
            {
                return BadRequest();
            }
            
            BookingResponseDTO bookingResponseDTO = new EventResponseDTO();

            return CreatedAtAction(nameof(PostNewEvent), eventResponse);
        }

    }
}
