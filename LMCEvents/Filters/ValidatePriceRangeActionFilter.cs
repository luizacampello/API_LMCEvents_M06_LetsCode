using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LMCEvents.Filters
{
    public class ValidatePriceRangeActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            decimal priceMin = (decimal)context.ActionArguments["priceMin"];
            decimal priceMax = (decimal)context.ActionArguments["priceMax"];
           
            if (priceMin > priceMax)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
