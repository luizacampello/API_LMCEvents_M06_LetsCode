using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Service;
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class EventReservationController : ControllerBase
    {
        public IEventReservationService _eventReservationService;

        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        [HttpGet("/bookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "admin, cliente")]
        public ActionResult<List<BookingResponseDTO>> GetBookings()
        {
            List<BookingResponseDTO> bookingResponse = _eventReservationService.GetBookings();

            if (bookingResponse.Count == 0)
            {
                return NoContent();
            }

            return Ok(bookingResponse);
        }

        [HttpGet("/bookingByPersonNameAndEventTitle/{eventTitle}/{personName}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin, cliente")]
        public ActionResult<List<BookingResponseDTO>> GetBookingByPersonNameAndEventTitle(string eventTitle, string personName)
        {
            List<BookingResponseDTO> bookingResponse = _eventReservationService.GetBookingByPersonNameAndEventTitle(personName, eventTitle);

            if (bookingResponse.Count == 0)
            {
                return NotFound();
            }

            return Ok(bookingResponse);
        }

        [HttpPost("/newBooking")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin, cliente")]
        public ActionResult<BookingResponseDTO> PostNewBooking(BookingResponseDTO booking)
        {
            if (!_eventReservationService.InsertBooking(booking))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(PostNewBooking), booking);
        }

        [HttpPut("/updateBooking/{idBooking}/{newQuantity}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(ValidateBookingIdActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateBooking(long idBooking, int newQuantity)
        {
            if (!_eventReservationService.UpdateBooking(idBooking, newQuantity))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

        [HttpDelete("/deleteBooking/{idBooking}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(ValidateBookingIdActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteBooking(long idBooking)
        {
            if (!_eventReservationService.DeleteBooking(idBooking))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

    }
}
