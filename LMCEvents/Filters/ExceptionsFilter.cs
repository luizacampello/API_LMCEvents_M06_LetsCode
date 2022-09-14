using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LMCEvents.Filters
{
    public class ExceptionsFilters : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ProblemDetails problem = new()
            {
                Status = 500,
                Type = context.Exception.GetType().Name,
                Title = "Erro inesperado. Tente novamente.",
                Detail = "Erro inesperado. Tente novamente."
            };

            Console.WriteLine($"Tipo da exceção {context.Exception.GetType().Name}, mensagem {context.Exception.Message}, stack trace {context.Exception.StackTrace}.");

            switch (context.Exception)
            {
                case SqlException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;

                    problem.Status = 503;
                    problem.Title = "Erro inesperado ao se comunicar com o banco de dados";
                    problem.Detail = "Erro inesperado ao se comunicar com o banco de dados";

                    context.Result = new ObjectResult(problem);
                    break;

                case ArgumentNullException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status417ExpectationFailed;

                    problem.Status = 417;
                    problem.Title = "Erro inesperado no sistema";
                    problem.Detail = "Erro inesperado no sistema";

                    context.Result = new ObjectResult(problem);
                    break;

                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(problem);
                    break;
            }
        }
    }
}
