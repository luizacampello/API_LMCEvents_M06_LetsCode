using LMCEvents.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LMCEvents.Filters
{
    public class ValidateEventIdActionFilter : ActionFilterAttribute
    {
        public ICityEventService _cityEventService;

        public ValidateEventIdActionFilter(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idEvent = (long)context.ActionArguments["idEvent"];

            if (_cityEventService.GetEventById(idEvent) is null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }
    }
}
