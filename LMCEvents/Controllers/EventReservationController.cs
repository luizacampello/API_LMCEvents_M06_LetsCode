using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using LMCEvents.Core.Service;
using LMCEvents.DTOs;
using LMCEvents.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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

        [HttpGet("/bookingByPersonNameAndEventTitle/{eventTitle}/{personName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<BookingResponseDTO>> GetBookingByPersonNameAndEventTitle(string eventTitle, string personName)
        {
            return Ok(_eventReservationService.GetBookingByPersonNameAndEventTitle(personName, eventTitle));
        }

        [HttpPost("/newBooking")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ValidateBookingIdActionFilter))]
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
