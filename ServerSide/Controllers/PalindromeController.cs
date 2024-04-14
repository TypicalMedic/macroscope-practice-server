using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ServerSide.BusinessLogic.Interfaces;

namespace ServerSide.Controllers
{
    /// <summary>
    /// Предоставляет функционал для работы с палиндромами
    /// </summary>
    [ApiController]
    [EnableRateLimiting("Concurrency")]
    [Route("palindrome")]

    public class PalindromeController(IPalindromeService service) : ControllerBase
    {

        private readonly IPalindromeService _pService = service;

        /// <summary>
        /// Проверка является ли входящая строка палиндромом
        /// </summary>
        /// <response code="200">Возвращает true, если строка - палиндром, иначе false</response>
        /// <response code="400">Тело запроса не соответствует схеме</response>
        /// <response code="503">Сервис не готов обработать запрос</response>
        [HttpPost("check")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
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
