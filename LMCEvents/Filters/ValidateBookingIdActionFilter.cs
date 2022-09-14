using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using LMCEvents.Core.Interfaces;

namespace LMCEvents.Filters
{
    public class ValidateBookingIdActionFilter : ActionFilterAttribute
    {
        public IEventReservationService _reservationService;

        public ValidateBookingIdActionFilter(IEventReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idBooking = (long)context.ActionArguments["idBooking"];

            if (_reservationService.GetBookingById(idBooking) is null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
