using LMCEvents.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LMCEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class TokenController : ControllerBase
    {
        public ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> CreateToken(string name, string permission)
        {
            string token = _tokenService.GenerateTokenEvents(name, permission);

            return Ok(token);
        }
    }
}
