using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ServerSide.Controllers.DTOs;
using ServerSide.Controllers.Interfaces;

namespace ServerSide.Controllers
{
    [ApiController]
    [EnableRateLimiting("Concurrency")]
    [Route("palindrome")]
    public class PalindromeController : ControllerBase
    {

        private readonly ILogger<PalindromeController> _logger;
        private readonly IPalindromeService _ignat;

        public PalindromeController(ILogger<PalindromeController> logger, IPalindromeService service)
        {
            _logger = logger;
            _ignat = service;
        }

        [HttpGet("check")]
        [Consumes("application/json")]
        public async Task<ActionResult<bool>> Check([FromBody] PalindromeDTO input)
        {
            if(input.Value == null)
            {
                return BadRequest();
            }

            return await _ignat.IsPalindrome(input.Value);
        }
    }
}
