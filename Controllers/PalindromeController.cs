using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ServerSide.Controllers.Interfaces;

namespace ServerSide.Controllers
{
    /// <summary>
    /// Предоставляет функционал для работы с палиндромами
    /// </summary>
    [ApiController]
    [EnableRateLimiting("Concurrency")]
    [Route("palindrome")]

    public class PalindromeController(ILogger<PalindromeController> logger, IPalindromeService service) : ControllerBase
    {

        private readonly ILogger<PalindromeController> _logger = logger;
        private readonly IPalindromeService _pService = service;

        /// <summary>
        /// Проверка является ли входящая строка палиндромом
        /// </summary>
        /// <response code="200">Возвращает true, если строка - палиндром, иначе false</response>
        /// <response code="400">Тело запроса не соответствует схеме</response>
        [HttpPost("check")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> Check([FromBody]string? input)
        {
            if(input == null)
            {
                return BadRequest();
            }

            return await _pService.IsPalindrome(input);
        }
    }
}
