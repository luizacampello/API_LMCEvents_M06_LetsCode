using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
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

    }
}
